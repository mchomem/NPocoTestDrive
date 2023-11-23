namespace NPocoTestDrive.Domain.Dtos
{
    public class EmployeeDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? DocumentNumber { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime? UpdatedIn { get; set; }
    }
}
