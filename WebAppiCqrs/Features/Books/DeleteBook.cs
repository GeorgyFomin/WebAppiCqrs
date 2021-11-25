using MediatR;
using WebAppiCqrs.Data;

namespace WebAppiCqrs.Features.Books
{
    public class DeleteBook
    {
        public class Command:IRequest
        {
            public int Id { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly BookContext _db;
            public CommandHandler(BookContext db) => _db = db;
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _db.Books.FindAsync(request.Id);
                if (book == null) return Unit.Value;
                _db.Books.Remove(book);
                await _db.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
