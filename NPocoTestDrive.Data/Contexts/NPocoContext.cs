﻿using NPoco;
using NPoco.FluentMappings;
using NPoco.SqlServer;
using NPocoTestDrive.Data.Mappings;
using NPocoTestDrive.Domain.Models;

namespace NPocoTestDrive.Data.Contexts
{
    public class NPocoContext
    {
        public DatabaseFactory? DbFactory { get; set; }

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
