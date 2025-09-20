using Alx.Repo.Contracts.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Alx.Repo.Api.SwaggerExamples
{
    public class CreateItemDtoExample : IExamplesProvider<CreateItemDto>
    {
        public CreateItemDto GetExamples()
        {
            return new CreateItemDto(
                    "Item",
                    "fa079815-1a1a-4030-97e2-dda7c6858385",
                    0,
                    "Description",
                    "Domain",
                    "Content",
                    DateTime.Now,
                    DateTime.Now,
                    "fa079815-1a1a-4030-97e2-dda7c6858385",
                    "fa079815-1a1a-4030-97e2-dda7c6858385"
                );
        }
    }
}

