using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueApp3.Server.Datas;
using VueApp3.Server.Models;

namespace VueApp3.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly TestDbContext _context;

        public ProductController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            return _context.Product.ToList();
        }

        [HttpGet("id")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPost]

        public ActionResult<Product> PostProduct(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof (GetProduct), new {id = product.Id}, product);
        }

        [HttpPut]

        public ActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]

        public ActionResult DeleteProduct(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null) { 
            return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
