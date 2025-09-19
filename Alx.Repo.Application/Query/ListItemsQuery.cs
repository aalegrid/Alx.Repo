using Alx.Repo.Contracts.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Query
{
    public record ListItemsQuery : IRequest<List<GetItemDto>>;
    

    public class ListItemssQueryHandler(ApplicationDbContext context) : IRequestHandler<ListItemsQuery, List<GetItemDto>>
    {
        public async Task<List<GetItemDto>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
        {
            return await context.Items
                .Select(p => new GetItemDto(p.Id, p.Name, p.UserId, p.ParentId, p.Description, p.Domain, p.Content, p.AuditCreatedOn, p.AuditLastUpdated, p.AuditCreatedByUser, p.AuditLastUpdatedByUser))
                .ToListAsync();
        }
    }

}

