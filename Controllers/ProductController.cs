using EcommerceApp.Data;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controller
{
    [Route("api/[controller]")]
    public class ProductsController(AppDbContext _context) : ControllerBase
    {
        private readonly AppDbContext context = _context;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var listofproducts = context.Products.ToList();
            return Ok(listofproducts);
        }


        [HttpPost]
        public IActionResult InsertProduct(string productName, int qty, decimal price)
        {
            var product = new Product
            {
                ProductName = productName,
                Quantity = qty,
                Price = price
            };
            context.Products.Add(product);
            context.SaveChanges();
            return Ok("Product Added Successfully");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return Ok("Product Deleted");
            }
            return Ok("No Product found with the id:" + id);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound("No product found");
            }
            product.ProductName = updatedProduct.ProductName;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;

            context.SaveChanges();
            return Ok(product);
        }
    }
}