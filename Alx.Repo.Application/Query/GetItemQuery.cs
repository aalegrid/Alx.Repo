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
    public record GetItemQuery(int Id) : IRequest<GetItemDto>;

    public class GetItemQueryHandler(ApplicationDbContext context) : IRequestHandler<GetItemQuery, GetItemDto?>
    {
        public async Task<GetItemDto?> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(request.Id);
            if (item == null)
            {
                return null;
            }
            return new GetItemDto(item.Id, item.Name, item.UserId, item.ParentId, item.Description, item.Domain, item.Content, item.AuditCreatedOn, item.AuditLastUpdated, item.AuditCreatedByUser, item.AuditLastUpdatedByUser);
        }
    }
}
