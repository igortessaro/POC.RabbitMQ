using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC.RabbitMQ.Domain.Command.Customer;
using POC.RabbitMQ.Domain.DataObjectTransfer;
using POC.RabbitMQ.Domain.Factories;
using POC.RabbitMQ.Domain.Services;
using RabbitMQ.Client;

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

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerCommand customer)
        {
            this.CustomerService.ReceiveCustomer(customer);
            return Ok(new ResponseDto<string>(true, "OK"));
        }
    }
}