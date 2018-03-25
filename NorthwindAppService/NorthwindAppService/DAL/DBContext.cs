using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAppService.DAL
{
    public class DBContext : IDisposable
    {
        private bool disposed = false;

        protected SqlConnection connection;

        private static IConfigurationRoot _configuration;
        //Server=tcp:northwinddatabase.database.windows.net,1433;Initial Catalog=northwind;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        //var connectionStr = "data source=(localdb)\\MSSQLLocalDB;Database=northwind;integrated security=True;MultipleActiveResultSets=True;App=Dapper";
        public DBContext()
        {
            _configuration = new ConfigurationBuilder()
                                                    .AddJsonFile("appsettings.json")
                                                    .AddEnvironmentVariables()
                                                    .Build();

            var connectionStr = _configuration["ConnectionStrings:NorthWindDB"];
            connection = new SqlConnection(connectionStr);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                connection.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
