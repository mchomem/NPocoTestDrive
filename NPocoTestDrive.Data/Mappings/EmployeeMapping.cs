namespace NPocoTestDrive.Data.Mappings
{
    public class EmployeeMapping : Map<Employee>
    {
        public EmployeeMapping()
        {
            this.TableName("Employee");
            this.PrimaryKey("Id");
            this.Columns(x =>
            {
                x.Column(c => c.Id);
                x.Column(c => c.Name);
                x.Column(c => c.DocumentNumber);
                x.Column(c => c.Active);
                x.Column(c => c.CreatedIn);
                x.Column(c => c.UpdatedIn);
            });
        }
    }
}
