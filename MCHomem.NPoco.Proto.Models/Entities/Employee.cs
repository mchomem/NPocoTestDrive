using System;

namespace MCHomem.NPoco.Proto.Models.Entities
{
    public class Employee
    {
        public Int32? EmployeeID { get; set; }
        public String Name { get; set; }
        public String DocumentNumber { get; set; }
        public Boolean? Active { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime? UpdatedIn { get; set; }
    }
}
