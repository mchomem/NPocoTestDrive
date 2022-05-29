using MCHomem.NPoco.Proto.Models.Contexts;
using MCHomem.NPoco.Proto.Models.Entities;
using NPoco;
using System.Collections.Generic;
using System.Text;

namespace MCHomem.NPoco.Proto.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public void Create(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                db.Insert(entity);
            }
        }

        public void Delete(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                db.Delete(entity);
            }
        }

        public Employee Details(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                return db.SingleOrDefaultById<Employee>(entity.EmployeeID);
            }
        }

        public List<Employee> Retreave(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select");
                sql.Append(" *");
                sql.Append(" from");
                sql.Append(" Employee");
                sql.Append(" where");
                sql.Append(" (@0 is null or EmployeeID = @0)");
                sql.Append(" and (@1 is null or [Name] like '%' + @1 + '%')");
                sql.Append(" and (@2 is null or DocumentNumber = @2)");
                sql.Append(" and (@3 is null or Active = @3)");

                return db.Fetch<Employee>
                    (
                        sql.ToString()
                        , entity?.EmployeeID
                        , entity?.Name
                        , entity?.DocumentNumber
                        , entity?.Active
                    );
            }
        }

        public void Update(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                db.Update(entity);
            }
        }
    }
}
