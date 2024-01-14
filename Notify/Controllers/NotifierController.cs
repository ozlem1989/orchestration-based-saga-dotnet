using Microsoft.AspNetCore.Mvc;
using System;

namespace Notifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifierController : ControllerBase
    {

        [HttpPost]
        public int Post([FromBody] Notifier notifier)
        {
            Console.WriteLine($"Send notification for : {notifier.ProductName}");
            return 3;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine($"Sent rollback transaction notification: {id}");
        }
    }
}
