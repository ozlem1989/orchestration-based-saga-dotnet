using Microsoft.AspNetCore.Mvc;
using System;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] Inventory inventory)
        {
            throw new Exception("Error  updating inventory");
            Console.WriteLine($"Updated inventory for : {inventory.ProductName}");
            return 2;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine($"Deleted inventory : {id}");
        }
    }
}
