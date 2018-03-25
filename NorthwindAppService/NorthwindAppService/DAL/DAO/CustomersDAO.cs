using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAppService.DAL
{
    public class CustomersDAO : DBContext
    {
        public CustomersDAO()
        {
            connection.Open();
        }

        public IEnumerable<Customers> GetCustomer()
        {
            string str = $"SELECT * FROM dbo.Customers";
            var query = connection.Query<Customers>(str);
            connection.Dispose();

            return query;
        }


            public Customers GetCustomer(string ID)
        {
            string str = $"SELECT * FROM dbo.Customers WHERE CustomerID ='{ID}' ";
            var query = connection.Query<Customers>(str).FirstOrDefault();
            connection.Dispose();

            return query;
        }
    }
}
