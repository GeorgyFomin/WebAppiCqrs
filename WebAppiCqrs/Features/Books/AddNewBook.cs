using MediatR;
using WebAppiCqrs.Data;
using WebAppiCqrs.Models;

namespace WebAppiCqrs.Features.Books
{
    public class AddNewBook
    {
        public class Command:IRequest<int>
        {
            public string Title { get; set;             }
            public string Author { get; set; }
            public string Description { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly BookContext _db;

            public CommandHandler(BookContext db) => _db = db;
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Book
                {
                    Title = request.Title,
                    Author = request.Author,
                    Description = request.Description
                };
                await _db.Books.AddAsync(entity, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
