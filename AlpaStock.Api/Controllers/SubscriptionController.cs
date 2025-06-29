using AlpaStock.Core.DTOs.Request.Subscription;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AlpaStock.Api.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateSubscriptions(AddSubPlanReq req)
        {

            var result = await _subscriptionService.CreateSubscriptionPlan(req);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("retrieve/all")]
        public async Task<IActionResult> GetAllSubscriptions()
        {

            var result = await _subscriptionService.RetrieveAllPlan();

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetSubscriptionAsync(string id)
        {
            var result = await _subscriptionService.RetrievePlanFeature(id);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSubscriptionAsync(UpdateSubDetails req)
        {

            var result = await _subscriptionService.updateSubscriptionPlanDetails(req);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("feature/update")]
        public async Task<IActionResult> UpdateSubscriptionFeatureAsync(UpdateSubScriptionFeature req)
        {

            var result = await _subscriptionService.updateSubscriptionFeature(req);

            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


    }
}
