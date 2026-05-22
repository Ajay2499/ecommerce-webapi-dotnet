using EcommerceApp.Data;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EcommerceApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        public readonly AppDbContext context;
        public CustomerController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var customers = context.Customers.ToList();
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Customers.Add(customer);
            context.SaveChanges();
            return Ok("Customer saved Successfully");
        }
    }
}