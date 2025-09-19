using Alx.Repo.Application.Auth.Model;
using JetBrains.Annotations;
using MediatR;
using Swashbuckle.AspNetCore.Filters;

namespace Alx.Repo.Api.SwaggerExamples
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]

    public class LoginExample
    {
        public class CreateLoginExample() : IExamplesProvider<LoginUser>
        {
            public LoginUser GetExamples() => new LoginUser
            {
                Email = "alex@alegrid.com",
                Password = "password"
            };
        }
    }
}
