using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Alx.Repo.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? AuditCreatedOn { get; set; }
        public DateTime? AuditLastUpdated { get; set; }
        [MaxLength(255)]
        public string AuditCreatedByUser { get; set; }
        [MaxLength(255)]
        public string AuditLastUpdatedByUser { get; set; }
    }
}
