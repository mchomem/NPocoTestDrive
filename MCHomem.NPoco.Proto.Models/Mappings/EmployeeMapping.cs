using MCHomem.NPoco.Proto.Models.Entities;
using NPoco.FluentMappings;

namespace MCHomem.NPoco.Proto.Models.Mappings
{
    public class EmployeeMapping : Map<Employee>
    {
        public EmployeeMapping()
        {
            this.TableName("Employee");
            this.PrimaryKey("EmployeeID");
            this.Columns(x =>
            {
                x.Column(c => c.EmployeeID);
                x.Column(c => c.Name);
                x.Column(c => c.DocumentNumber);
                x.Column(c => c.Active);
                x.Column(c => c.CreatedIn);
                x.Column(c => c.UpdatedIn);
            });
        }
    }
}
