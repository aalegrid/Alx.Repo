using Alx.Repo.Contracts.Dto;
using Alx.Repo.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Alx.Repo.Application.Query
{
    // Query to list all items, returns a list of ItemDto, no parameters needed
    public record ListItemsQuery : IRequest<List<ItemDto>>;

    // Handler for ListItemsQuery - handler takes a ListItemsQuery (defined above) and returns a List<ItemDto>
    // Has Injected 'ApplicationDbContext context' as parameter to access the database, and 'IMapper mapper' to map between data models and DTOs
    public class ListItemssQueryHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<ListItemsQuery, List<ItemDto>>
    {
        // Handles the Query (ListItemsQuery) and returns a List<ItemDto>
        public async Task<List<ItemDto>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
        {
            //return await context.Items
            //    .Select(p => new ItemDto(p.Id, p.Name, p.UserId, p.ParentId, p.Description, p.Domain, p.Content, p.AuditCreatedOn, p.AuditLastUpdated, p.AuditCreatedByUser, p.AuditLastUpdatedByUser))
            //    .ToListAsync();

            return await context.Items
                .Select(p => mapper.Map<ItemDto>(p))
                .ToListAsync();
        }
    }

}

