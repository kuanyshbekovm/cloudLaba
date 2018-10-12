using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using System.Linq;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    public class WebApiController : Controller
    {
        private readonly WebApiContext _context;

        public WebApiController(WebApiContext context)
        {
            _context = context;

            if (_context.WebApiItem.Count() == 0)
            {
                _context.WebApiItem.Add(new WebApiClass { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<WebApiClass> GetAll()
        {
            return _context.WebApiItem.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.WebApiItem.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] WebApiClass item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.WebApiItem.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] WebApiClass item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _context.WebApiItem.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            System.Console.WriteLine(todo.Name);
            _context.WebApiItem.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.WebApiItem.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.WebApiItem.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
    // abylay.kairbekov@gmail.com
}       
   