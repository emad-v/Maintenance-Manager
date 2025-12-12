namespace MaintenanceManager.DomainModel.Models.Customers
{
    public class CustomerResponseDto
    {

        public required string Reference { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}