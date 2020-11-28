using MCHomem.NPoco.Proto.Models.Mappings;
using NPoco;
using NPoco.FluentMappings;
using NPoco.SqlServer;

namespace MCHomem.NPoco.Proto.Models.Repositories
{
    public class TestAppContext
    {
        public DatabaseFactory DbFactory { get; set; }

        public Database Get()
        {
            return new SqlServerDatabase(AppSettings.Get("DataConnection"));
        }

        public void Setup()
        {
            FluentConfig fluentConfig = FluentMappingConfiguration.Configure(new EmployeeMapping());

            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new SqlServerDatabase(AppSettings.Get("DataConnection")));
                x.WithFluentConfig(fluentConfig);
                x.WithMapper(new Mapper());
            });
        }
    }
}
