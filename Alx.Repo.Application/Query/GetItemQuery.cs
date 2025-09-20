using Alx.Repo.Contracts.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Alx.Repo.Application.Query
{
    // This query takes an integer Id and returns an ItemDto
    public record GetItemQuery(int Id) : IRequest<ItemDto>;

    // Handler for GetItemQuery - handler takes a GetItemQuery (defined above) and returns an ItemDto
    // Has Injected 'ApplicationDbContext context' as parameter to access the database
    public class GetItemQueryHandler(ApplicationDbContext context) : IRequestHandler<GetItemQuery, ItemDto?>
    {
        // Handles the Query (GetItemQuery) and returns an ItemDto
        public async Task<ItemDto?> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(request.Id);
            if (item == null)
            {
                return null;
            }
            return new ItemDto(item.Id, item.Name, item.UserId, item.ParentId, item.Description, item.Domain, item.Content, item.AuditCreatedOn, item.AuditLastUpdated, item.AuditCreatedByUser, item.AuditLastUpdatedByUser);
        }
    }
}
