using NPoco;
using System;

namespace MCHomem.NPoco.Proto.Models.Entities
{
    [TableName("Employee")]
    [PrimaryKey("EmployeeID")]
    public class Employee
    {
        public Int32? EmployeeID { get; set; }
        [Column("Name")]
        public String Name { get; set; }
        [Column("DocumentNumber")]
        public String DocumentNumber { get; set; }
        [Column("Active")]
        public Boolean? Active { get; set; }
        [Column("CreatedIn")]
        public DateTime CreatedIn { get; set; }
        [Column("UpdatedIn")]
        public DateTime? UpdatedIn { get; set; }
    }
}
