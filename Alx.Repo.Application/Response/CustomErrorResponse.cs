using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Response
{
    public class CustomErrorResponse
    {
        public string? Message { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
