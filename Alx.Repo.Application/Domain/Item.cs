using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Alx.Repo.Domain
{
    public class Item : BaseEntity
    {

        public string Name { get; set; }
        public string UserId { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public string Content { get; set; }

    }
}
