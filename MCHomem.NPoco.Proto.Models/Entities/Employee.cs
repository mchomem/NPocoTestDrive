using System;

namespace MCHomem.NPoco.Proto.Models.Entities
{
    public class Employee
    {
        public int? EmployeeID { get; set; }
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime? UpdatedIn { get; set; }
    }
}
