using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Alx.Repo.Application.Command
{
    // This command takes an integer Id and returns nothing (void)
    public record DeleteItemCommand(int Id) : IRequest;

    // Handler for DeleteItemCommand - handler takes a DeleteItemCommand (defined above) and returns nothing (void)
    // Has Injected 'ApplicationDbContext context' as parameter to access the database
    public class DeleteItemCommandHandler(ApplicationDbContext context) : IRequestHandler<DeleteItemCommand>
    {
        // Handle method to process the command
        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(request.Id);
            if (item == null)
            {
                throw new InvalidOperationException($"Item with Id {request.Id} not found.");
            }
            context.Items.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
