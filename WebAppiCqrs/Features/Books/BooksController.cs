using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppiCqrs.Models;

namespace WebAppiCqrs.Features.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator) => _mediator = mediator;
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks() => await _mediator.Send(new GetBooks.Query());
        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id) => await _mediator.Send(new GetBookById.Query() { Id = id });
        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] AddNewBook.Command command)
        {
            var createBookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBook), new { id = createBookId }, null);
        }
        //public async Task<ActionResult> DeleteBook(int id)
        //{
        //    await _mediator.Send(new DeleteBook.Command { Id = id });
        //    return NoContent();
        //}
    }
}
