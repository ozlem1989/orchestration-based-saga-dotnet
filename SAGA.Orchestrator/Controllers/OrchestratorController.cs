using Microsoft.AspNetCore.Mvc;
using SAGA.Orchestrator.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SAGA.Orchestrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrchestratorController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrchestratorController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ResponseDto> Post(RequestDto order)
        {
            var request = JsonSerializer.Serialize(order); 

            // create order
            var orderClient = httpClientFactory.CreateClient("Order");
            var orderResponse = await orderClient.PostAsync("/api/order",
                new StringContent(request, Encoding.UTF8, "application/JSON"));
            var orderId = await orderResponse.Content.ReadAsStringAsync();

            // update inventory
            string inventoryId = string.Empty;
            try
            {
                var inventoryClient = httpClientFactory.CreateClient("Inventory");
                var inventoryResponse = await inventoryClient.PostAsync("/api/inventory",
                    new StringContent(request, Encoding.UTF8, "application/JSON"));

                if (inventoryResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(inventoryResponse.ReasonPhrase);

                inventoryId = await inventoryResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                await orderClient.DeleteAsync($"/api/order/{orderId}");
                return new ResponseDto
                {
                    Success = false,
                    Reason = ex.Message
                };
            }

            // send notification
            var notifierClient = httpClientFactory.CreateClient("Notifier");
            var notifierResponse = await notifierClient.PostAsync("/api/notifier",
                new StringContent(request, Encoding.UTF8, "application/JSON"));
            var notifierId = await notifierResponse.Content.ReadAsStringAsync();

            Console.WriteLine($"Order: {orderId}  Inventory: {inventoryId} Notifier: {notifierId}");

            return new ResponseDto
            {
                Success = true,
                OrderId = orderId
            };
        }
    }
}
