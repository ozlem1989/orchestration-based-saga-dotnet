using Microsoft.AspNetCore.Mvc;
using Orders.Models;
using System;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] Order order)
        {
            Console.WriteLine($"Order for : {order.ProductName}");
            return 1;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine($"Deleted order : {id}");
        }
    }
}
