
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using MaintenanceManager.Business;
using MaintenanceManager.Data;
using MaintenanceManager.Data.Repositories;
using MaintenanceManager.Interfaces.Repositories;
using MaintenanceManager.Web.API.Controllers;
using ManitenanceManager.Web.API.Controllers;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace MaintenanceManager.Web.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);//// This line automatically loads appsettings.{Environment}.json
            Console.WriteLine($"Environment: ----------------------------------->>   {builder.Environment.EnvironmentName}");

            builder.Services.AddDbContext<MaintenanceManagerDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));//injecting the database

            builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var config = builder.Configuration.GetSection("AWS");
                var credentials = new SessionAWSCredentials(
                    config["AccessKey"],
                    config["SecretKey"],
                    config["SessionToken"]
                    );
                return new AmazonDynamoDBClient(credentials, RegionEndpoint.EUWest1);
            });

            builder.Services.AddScoped<DynamoDBContext>();


            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IMachineRepository, MachineRepository>();
            builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
            builder.Services.AddScoped<IUsageCounterRepository, UsageCounterRepository>();
            builder.Services.AddScoped<IMaintenanceRuleRepository, MaintenanceRuleRepository>();
            builder.Services.AddScoped<IComponentRuleStatusRepository, ComponentRuleStatusRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserCustomerRepository, UserCustomerRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>(); //postgres implmenetation
            builder.Services.AddScoped<IMaintenanceLogRepository, MaintenanceLogRepository>(); //postgres implemnetation

            builder.Services.AddScoped<INotificationRepository, NotificationDynamoDbRepository>();
            builder.Services.AddScoped<IMaintenanceLogRepository, MaintenanceLogDynamoDbRepository>();




            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<MachineService>();
            builder.Services.AddScoped<MaintenanceRuleService>();
            builder.Services.AddScoped<ComponentRuleStatusService>();
            builder.Services.AddScoped<UsageCounterService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserCustomerService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<MaintenanceLogService>();


            builder.Services.AddScoped<CustomerController>();
            builder.Services.AddScoped<MachineController>();
            builder.Services.AddScoped<MaintenanceRuleController>();
            builder.Services.AddScoped<ComponentController>();
            builder.Services.AddScoped<CounterController>();
            builder.Services.AddScoped<UserCustomerController>();
            builder.Services.AddScoped<NotificationController>();
            builder.Services.AddScoped<MaintenanceLogController>();

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build(); // app starts, services registered 


            //data migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MaintenanceManagerDbContext>();
                dbContext.Database.Migrate();

                if (app.Environment.IsEnvironment("Docker"))
                {
                    
                    var connectionString = dbContext.Database.GetConnectionString();
                    var sqlFilePath = "/app/Database/seed_data.sql";

                    Console.WriteLine($"Checking file: {sqlFilePath}");
                    Console.WriteLine($"File exists: {File.Exists(sqlFilePath)}");

                    if (File.Exists(sqlFilePath))
                    {
                        
                        var sql = File.ReadAllText(sqlFilePath);
                        

                        using (var connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            
                            using (var command = new NpgsqlCommand(sql, connection))
                            {
                                command.ExecuteNonQuery();
                                
                            }
                        }
                    }


                    
                }
             

            }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsEnvironment("Docker"))
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MaintenanceManager API v1");
                        options.RoutePrefix = string.Empty; // Serve at root "/"
                    });
                }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
