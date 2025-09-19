using Alx.Repo.Contracts.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Alx.Repo.Application.Query
{
    public record GetItemQuery(int Id) : IRequest<ItemDto>;

    public class GetItemQueryHandler(ApplicationDbContext context) : IRequestHandler<GetItemQuery, ItemDto?>
    {
        public async Task<ItemDto?> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(request.Id);
            if (item == null)
            {
                return null;
            }
            return new ItemDto(item.Id, item.Name, item.UserId, item.ParentId, item.Description, item.Domain, item.Content);
        }
    }
}
