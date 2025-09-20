using Alx.Repo.Contracts.Dto;
using Alx.Repo.Domain;
using AutoMapper;
using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Command
{
    // This command edits an existing item in the database, accepts ItemDto with updated fields and Id of the item to edit. It returns the updated item as a DTO.
    public record EditItemCommand(ItemDto editItem, int Id) : IRequest<ItemDto>;

    // Handler declaration for EditItemCommand - handler takes an EditItemCommand (defined above) and returns an ItemDto (the updated item)
    // Has Injected 'ApplicationDbContext context' and 'IMapper mapper' as parameters to access the database and map entities to DTOs
    public class EditItemCommandCommandHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<EditItemCommand, ItemDto>
    {
        // Handle method to process the command
        public async Task<ItemDto> Handle(EditItemCommand command, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(command.Id);

            // If item not found, throw an exception
            if (item == null)
            {
                throw new InvalidOperationException($"Item with Id {command.Id} not found.");
            }
            context.Entry(item).CurrentValues.SetValues(command.editItem); // Apply DTO values
            context.Update(item);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<ItemDto>(item);

        }
    }
}
