using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application.Auth.Model
{
    public class RegisterUser
    {

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string AppDomain { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
