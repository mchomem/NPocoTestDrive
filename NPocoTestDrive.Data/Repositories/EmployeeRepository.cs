using NPoco;
using NPocoTestDrive.Data.Contexts;
using NPocoTestDrive.Data.Repositories.Interfaces;
using NPocoTestDrive.Domain.Entities;
using System.Text;

namespace NPocoTestDrive.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task Create(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {  
                await db.InsertAsync(entity);
            }
        }

        public async Task Delete(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                await db.DeleteAsync(entity);
            }
        }

        public async Task<Employee> Details(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                return await db.SingleOrDefaultByIdAsync<Employee>(entity.EmployeeID);
            }
        }

        public async Task<List<Employee>> Retreave(Employee entity)
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

                return await db.FetchAsync<Employee>
                    (
                        sql.ToString()
                        , entity?.EmployeeID
                        , entity?.Name
                        , entity?.DocumentNumber
                        , entity?.Active
                    );
            }
        }

        public async Task Update(Employee entity)
        {
            using (IDatabase db = NPocoContext.Get())
            {
                await db.UpdateAsync(entity);
            }
        }
    }
}
