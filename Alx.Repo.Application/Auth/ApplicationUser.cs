﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string AppDomain { get; set; }
    }
}
