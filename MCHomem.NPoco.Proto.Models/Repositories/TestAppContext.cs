using NPoco;
using NPoco.SqlServer;

namespace MCHomem.NPoco.Proto.Models.Repositories
{
    public class TestAppContext
    {
        public Database Get()
        {
            return new SqlServerDatabase(AppSettings.Get("DataConnection"));
        }
    }
}
