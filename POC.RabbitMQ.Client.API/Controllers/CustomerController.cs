using Microsoft.AspNetCore.Mvc;
using POC.RabbitMQ.Domain.Command.Customer;
using POC.RabbitMQ.Domain.DataObjectTransfer;
using POC.RabbitMQ.Domain.Services;

namespace POC.RabbitMQ.Client.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService CustomerService { get; set; }

        public CustomerController(ICustomerService customerService)
        {
            this.CustomerService = customerService;
        }

        [HttpGet]
        public IActionResult GetSuccess()
        {
            return Ok(new ResponseDto<string>(true, "OK"));
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            QueueStatusDto result = this.CustomerService.GetQueueStatus();
            return Ok(new ResponseDto<QueueStatusDto>(true, result));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerCommand customer)
        {
            this.CustomerService.ReceiveCustomer(customer);
            return Ok(new ResponseDto<string>(true, "OK"));
        }
    }
}