using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAppService.DAL
{
    public class OrdersDAO : DBContext
    {
        public OrdersDAO()
        {
            connection.Open();
        }

        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Orders Get(int ID)
        {
            string str = $@"SELECT * FROM dbo.Orders Where OrderID='{ID}'";

            var query = connection.Query<Orders>(str).FirstOrDefault();

            return query;
        }

        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int Create(Orders order)
        {

            string query = $@"INSERT INTO Orders([CustomerID],[EmployeeID],[OrderDate],
                                          [RequiredDate],[ShippedDate],[ShipVia],[Freight]
                                          ,[ShipName],[ShipAddress],[ShipCity],[ShipRegion]
                                          ,[ShipPostalCode],[ShipCountry]) 
                              Values (@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipVia,@Freight,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry);
                              SELECT @@IDENTITY as id";

            var ID = connection.Query<int>(query, order).Single();

            return ID;
        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Update(Orders order)
        {
            string query = $@"UPDATE Orders SET ShipCountry = @ShipCountry
                              Where OrderID='{order.OrderID}'";

            var count = connection.Execute(query, order);

            return count > 0;
        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool Delete(int orderID)
        {
            string query = $@"DELETE FROM Orders Where OrderID ='{orderID}'";

            var count = connection.Execute(query, orderID);

            return count > 0;

        }

        /// <summary>
        /// 查詢是否存在
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool IsExists(int ID)
        {
            string str = $@"SELECT Count FROM dbo.Orders Where OrderID='{ID}'";

            var query = connection.Query<Orders>(str).Any();

            connection.Dispose();

            return query;
        }

    }
}
