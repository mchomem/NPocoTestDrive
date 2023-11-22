namespace NPocoTestDrive.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NPocoContext _context;

        public EmployeeRepository(NPocoContext context)
            => _context = context;

        public async Task Create(Employee entity)
            => await _context.InsertAsync(entity);

        public async Task Delete(Employee entity)
            => await _context.DeleteAsync(entity);

        public async Task<Employee> Details(Employee? entity = null)
            => await _context.SingleOrDefaultByIdAsync<Employee>(entity?.Id);

        public async Task<Employee> DetailsSql(Employee? entity = null)
            => await _context
                .QueryAsync<Employee>().Where(x => x.Id == entity.Id)
                .First();

        public async Task<List<Employee>> Retreave(Employee? entity = null)
        {
            if (entity != null)
                return await _context
                    .Query<Employee>()
                    .Where(x =>
                    (
                         (string.IsNullOrEmpty(entity.Name) || x.Name.Contains(entity.Name))
                         && (string.IsNullOrEmpty(entity.DocumentNumber) || x.DocumentNumber == entity.DocumentNumber)
                    ))
                    .ToListAsync();
            else
                return await _context
                    .Query<Employee>()
                    .ToListAsync();
        }

        public async Task<List<Employee>> RetreaveSql(Employee? entity = null)
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

            return await _context.FetchAsync<Employee>
                (
                    sql.ToString()
                    , entity?.Id
                    , entity?.Name
                    , entity?.DocumentNumber
                    , entity?.Active
                );
        }

        public async Task Update(Employee entity)
            => await _context.UpdateAsync(entity);
    }
}
