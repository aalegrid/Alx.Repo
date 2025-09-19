using Alx.Repo.Application.Auth.Model;
using Alx.Repo.Application.Command;
using JetBrains.Annotations;
using MediatR;
using Swashbuckle.AspNetCore.Filters;

namespace Alx.Repo.Api.SwaggerExamples
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]

    public class ItemExample
    {
        public class CreateItemExample() : IExamplesProvider<CreateItemCommand>
        {

            public CreateItemCommand GetExamples() => new CreateItemCommand("Item", "fa079815-1a1a-4030-97e2-dda7c6858385", 0, "Description", "Domain", "Content", DateTime.Now, DateTime.Now, "fa079815-1a1a-4030-97e2-dda7c6858385", "fa079815-1a1a-4030-97e2-dda7c6858385");
        }
    }
}
