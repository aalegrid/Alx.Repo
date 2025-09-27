using Alx.Repo.Application.Auth.Model;
using Swashbuckle.AspNetCore.Filters;

namespace Alx.Repo.Api.SwaggerExamples
{
    public class LoginUserExample : IExamplesProvider<LoginUser>
    {
        private readonly IConfiguration _config;

        public LoginUserExample(IConfiguration config)
        {
            _config = config;
        }

        public LoginUser GetExamples()
        {
     
            return new LoginUser
            {
                Email = _config.GetSection("Users:Primary:Email").Value ?? throw new InvalidOperationException("Config[Users:Primary:Email] not found."),
                Password = _config.GetSection("Users:Primary:Password").Value ?? throw new InvalidOperationException("Config[Users:Primary:Password] not found."),
            };
        }
    }
}
