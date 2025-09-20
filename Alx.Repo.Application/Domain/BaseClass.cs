using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? AuditCreatedOn { get; set; }
        public DateTime? AuditLastUpdated { get; set; }
        [MaxLength(255)]
        public string AuditCreatedByUser { get; set; } = string.Empty;
        [MaxLength(255)]
        public string AuditLastUpdatedByUser { get; set; } = string.Empty;
    }
}
