using Alx.Repo.Contracts.Dto;
using Alx.Repo.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Command
{
    // This command takes a CreateItemDto and returns an ItemDto
    public record CreateItemCommand(CreateItemDto createItem) : IRequest<ItemDto>;

    // Handler for CreateItemCommand - handler takes a CreateItemCommand (defined above) and returns an ItemDto
    // Has Injected 'ApplicationDbContext context' as parameter to access the database, and 'IMapper mapper' to map between data models and DTOs

    public class CreateItemCommandHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateItemCommand, ItemDto>
    {
        // Handles the Command (CreateItemCommand) and returns an ItemDto
        public async Task<ItemDto> Handle(CreateItemCommand command, CancellationToken cancellationToken)
        {
            var item = mapper.Map<Item>(command.createItem);
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            return mapper.Map<ItemDto>(item);
        }
    }

}
