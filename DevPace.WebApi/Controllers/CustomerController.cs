using DevPace.Application.Services.Interfaces;
using DevPace.Domain.Models.CustomerModels;
using Microsoft.AspNetCore.Mvc;

namespace DevPace.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        #region Dependencies

        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;


        #endregion

        #region .ctor

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        #endregion

        #region CRUD

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
        {
            var customer = await _customerService.GetCustomer(id);

            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerFindNSortModel customerFindNSort)
        {
            var customer = await _customerService.GetCustomers(customerFindNSort);

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateModel customerCreate)
        {
            var customer = await _customerService.CreateCustomer(customerCreate);

            _logger.LogInformation("Customer created successfully.");

            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateModel customerUpdate)
        {
            await _customerService.UpdateCustomer(customerUpdate);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromBody] CustomerDeleteModel customerDelete)
        {
            await _customerService.DeleteCustomer(customerDelete);

            return Ok();
        }

        #endregion

    }
}
