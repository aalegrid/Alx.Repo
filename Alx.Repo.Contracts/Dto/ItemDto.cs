using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Contracts.Dto
{
    public record ItemDto(int Id, string Name, string UserId, int ParentId, string Description, string Domain, string Content, DateTime? AuditCreatedOn, DateTime? AuditLastUpdated, string AuditCreatedByUser, string AuditLastUpdatedByUser);
}
