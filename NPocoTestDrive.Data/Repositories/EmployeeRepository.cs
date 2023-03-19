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
            using (IDatabase db = new NPocoContext().Get())
            {
                await db.InsertAsync(entity);
            }
        }

        public async Task Delete(Employee entity)
        {
            using (IDatabase db = new NPocoContext().Get())
            {
                await db.DeleteAsync(entity);
            }
        }

        public async Task<Employee> Details(Employee? entity = null)
        {
            using (IDatabase db = new NPocoContext().Get())
            {
                return await db.SingleOrDefaultByIdAsync<Employee>(entity.Id);
            }
        }

        public async Task<Employee> DetailsSql(Employee? entity = null)
        {
            throw new NotImplementedException("Method DetailsSql not implemented");
        }

        public async Task<List<Employee>> Retreave(Employee? entity = null)
        {
            using (IDatabase db = new NPocoContext().Get())
            {
                if (entity != null)
                    return await db
                        .Query<Employee>()
                        .Where(x =>
                        (
                             (string.IsNullOrEmpty(entity.Name) || x.Name.Contains(entity.Name))
                             && (string.IsNullOrEmpty(entity.DocumentNumber) || x.DocumentNumber == entity.DocumentNumber)                            
                        ))
                        .ToListAsync();
                else
                    return await db
                        .Query<Employee>()
                        .ToListAsync();
            }
        }

        public async Task<List<Employee>> RetreaveSql(Employee? entity = null)
        {
            using (IDatabase db = new NPocoContext().Get())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select");
                sql.Append(" Id");
                sql.Append(" ,Name");
                sql.Append(" ,DocumentNumber");
                sql.Append(" ,Active");
                sql.Append(" ,CreatedIn");
                sql.Append(" ,UpdatedIn");
                sql.Append(" from");
                sql.Append(" Employee");
                sql.Append(" where");
                sql.Append(" (@0 is null or Id = @0)");
                sql.Append(" and (@1 is null or [Name] like '%' + @1 + '%')");
                sql.Append(" and (@2 is null or DocumentNumber = @2)");
                sql.Append(" and (@3 is null or Active = @3)");

                return await db.FetchAsync<Employee>
                    (
                        sql.ToString()
                        , entity?.Id
                        , entity?.Name
                        , entity?.DocumentNumber
                        , entity?.Active
                    );
            }
        }

        public async Task Update(Employee entity)
        {
            using (IDatabase db = new NPocoContext().Get())
            {
                await db.UpdateAsync(entity);
            }
        }
    }
}
