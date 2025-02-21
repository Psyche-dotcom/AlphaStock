using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlpaStock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        [HttpPost("paypal/subscribe")]
        public IActionResult SubscribeToPayPal()
        {
            return Ok(new { message = "PayPal subscription successful", subscriptionId = "dummy-subscription-id" });
        }

        [HttpGet("subscriptions")]
        public IActionResult GetAllSubscriptions()
        {
            return Ok(new { message = "List of subscriptions", subscriptions = new string[] { "sub1", "sub2" } });
        }

        [HttpGet("subscription/{id}")]
        public IActionResult GetSubscription(string id)
        {
            return Ok(new { message = "Subscription details", subscriptionId = id, status = "active" });
        }

        [HttpPut("subscription/{id}")]
        public IActionResult UpdateSubscription(string id)
        {
            return Ok(new { message = "Subscription updated", subscriptionId = id });
        }

        [HttpDelete("subscription/{id}")]
        public IActionResult CancelSubscription(string id)
        {
            return Ok(new { message = "Subscription cancelled", subscriptionId = id });
        }
    }
}
