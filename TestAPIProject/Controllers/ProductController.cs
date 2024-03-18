using TestAPIProject.Data;
using TestAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ProductDBContext learnDBContext;
        public ProductController(ProductDBContext learnDB)
        {
            learnDBContext = learnDB;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Product students)
        {
            learnDBContext.Add(students);
            learnDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll()
        {
            List<Product> product = new List<Product>();
            product = learnDBContext.Products.ToList();
            return Ok(product);
        }

        [HttpPost("Update")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (id == 0 || id != product.ProductID)
            {
                return BadRequest(ModelState);
            }
            learnDBContext.Update(product);
            learnDBContext.SaveChanges();
            return Ok(product);
        }

        [HttpPost("GetById")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            Product product = learnDBContext.Products.FirstOrDefault(s => s.ProductID == id);
            learnDBContext.SaveChanges();
            return Ok(product);
        }
        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var student = learnDBContext.Products.FirstOrDefault(s => s.ProductID == id);
            learnDBContext.Remove(student);
            learnDBContext.SaveChanges();
            return Ok();
        }
    }
}

