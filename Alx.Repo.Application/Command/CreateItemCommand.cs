using Alx.Repo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Command
{
    public record CreateItemCommand(string Name, string UserId, int ParentId, string Description, string Domain, string Content, DateTime AuditCreatedOn, DateTime AuditLastUpdated, string AuditCreatedByUser, string AuditLastUpdatedByUser) : IRequest<int>;

    public class CreateItemCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateItemCommand, int>
    {
        public async Task<int> Handle(CreateItemCommand command, CancellationToken cancellationToken)
        {
            var item = new Item(command.Name, command.UserId, command.ParentId, command.Description, command.Domain, command.Content, command.AuditCreatedOn, command.AuditLastUpdated, command.AuditCreatedByUser, command.AuditLastUpdatedByUser);
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            return item.Id;
        }
    }

}
