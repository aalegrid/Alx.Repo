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
                Email = _config.GetSection("DefaultUser:Email").Value ?? throw new InvalidOperationException("Config[DefaultUser:Email] not found."),
                Password = _config.GetSection("DefaultUser:Password").Value ?? throw new InvalidOperationException("Config[DefaultUser:Password] not found."),
            };
        }
    }
}
