using Alx.Repo.Contracts.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Alx.Repo.Api.SwaggerExamples
{
    public class ItemDtoExample : IExamplesProvider<ItemDto>
    {
        public ItemDto GetExamples()
        {
            return new ItemDto(
                    1,
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

