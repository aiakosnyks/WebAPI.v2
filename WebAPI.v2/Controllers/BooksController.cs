using Book_Demo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.JsonPatch;
using Entities.Models;
using Repositories.EF_Core;

namespace Book_Demo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;
        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks() 
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
         
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
               var book = _context
              .Books
              .Where(x => x.Id.Equals(id))
              .SingleOrDefault();

                if (book is null)
                    return NotFound(); //404

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); //400
                }

                _context.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")]int id, 
        [FromBody] Book book)
        {
            //check book ? test
            var entity = _context
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

            if(entity is null)
            {
                return NotFound(); //404
            }
            
            //check id
            if(id!=book.Id)
                return BadRequest(); //400

            _context.Books.Remove(entity); 
            book.Id = entity.Id;
            _context.Books.Add(book);
            return Ok(book);

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")]int id)
        {

            var entity = _context
                .Books
                .Where(c => c.Id.Equals(id))
                .SingleOrDefault();

            if (entity is null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id:{id} could not found."
                }); //404

            _context.Books.Remove(entity); 
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            //check entity
            var entity = ApplicationContext.Books.Find(z => z.Id.Equals(id));
            if(entity is null)
                return NotFound(); //404

            bookPatch.ApplyTo(entity);

            return NoContent(); //204
        }
    }
}
