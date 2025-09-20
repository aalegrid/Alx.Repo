using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Domain
{
    public class Item : BaseEntity
    {
        public Item(string name, string userId, int parentId, string description, string domain, string content, DateTime? auditCreatedOn, DateTime? auditLastUpdated, string auditCreatedByUser, string auditLastUpdatedByUser)
        {
            Name = name;
            UserId = userId;
            ParentId = parentId;
            Description = description;
            Domain = domain;
            Content = content;
            AuditCreatedOn = auditCreatedOn;
            AuditLastUpdated = auditLastUpdated;
            AuditCreatedByUser = auditCreatedByUser;
            AuditLastUpdatedByUser = auditLastUpdatedByUser;
        }

        public string Name { get; set; }
        public string UserId { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Content { get; set; }

    }
}
