using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAppService.DAL;

namespace NorthwindAppService.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        //private CustomersDAO _customersDAO;
        private OrdersDAO _ordersDAO;

        public OrderController()
        {
            //_customersDAO = new CustomersDAO();
            _ordersDAO = new OrdersDAO();
        }


        // GET: api/Order/10248
        [HttpGet("{ID}")]
        public IActionResult Get(int ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var data = _ordersDAO.Get(ID);

                if (data == null)
                    return NotFound(data);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Order/10248
        [HttpPost]
        public IActionResult PostOrder([FromBody]Orders order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderId = _ordersDAO.Create(order);
                if (orderId > 0)
                {
                    return Created($"/api/Order/{orderId}", orderId);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Order/10248
        [HttpPut("{ID}")]
        public IActionResult PutOrder(int ID, [FromBody]Orders order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ID != order.OrderID)
            {
                return BadRequest();
            }

            try
            {
                _ordersDAO.Update(order);
            }
            catch (Exception)
            {
                if (!OrderExists(ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();

        }

        // DELETE: api/Order/5
        [HttpDelete("{ID}")]
        public IActionResult DeleteOrder(int ID)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var data = _ordersDAO.Get(ID);

                if (data == null)
                    return NotFound(data);

                return Ok(_ordersDAO.Delete(ID));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        private bool OrderExists(int ID)
        {
            return _ordersDAO.IsExists(ID);
        }
    }
}
