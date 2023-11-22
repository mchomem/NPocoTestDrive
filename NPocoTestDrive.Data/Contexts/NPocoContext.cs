namespace NPocoTestDrive.Data.Contexts
{
    public class NPocoContext : Database
    {
        public DatabaseFactory? DbFactory { get; set; }

        public NPocoContext() : base(AppSettings.SqlServerConnection, DatabaseType.SqlServer2012, SqlClientFactory.Instance)
        {
            this.Get();
        }

        public Database Get()
        {
            if (DbFactory == null)            
                Setup();

            if (DbFactory == null)
                throw new Exception("DbFactory is null");

            return DbFactory.GetDatabase();
        }

        private void Setup()
        {
            FluentConfig fluentConfig = FluentMappingConfiguration.Configure(new EmployeeMapping());

            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new SqlServerDatabase(AppSettings.SqlServerConnection));
                x.WithFluentConfig(fluentConfig);
                x.WithMapper(new Mapper());
            });
        }
    }
}
