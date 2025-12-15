
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

#Start with a clean Linux container

#.NET SDK is installed inside it

#The container filesystem is empty except for system files and the SDK


ARG BUILD_CONFIGURATION=Release
# cd /src - create if doesnt exist
WORKDIR /src 



# Copy only the service csproj
COPY ["src/MaintenanceManager.Web.Service/MaintenanceManager.Web.Service.csproj", "MaintenanceManager.Web.Service/"]



#Download all external dependencies that the project needs to compile
RUN dotnet restore "MaintenanceManager.Web.Service/MaintenanceManager.Web.Service.csproj" 


# copy from computer into the container 
COPY src/ .  


WORKDIR "MaintenanceManager.Web.Service"



RUN dotnet build "MaintenanceManager.Web.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build



# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MaintenanceManager.Web.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "MaintenanceManager.Web.Service.dll"]