using Alx.Repo.Application.Auth;
using Alx.Repo.Application.Auth.Model;
using Alx.Repo.Application.Command;
using Alx.Repo.Application.Query;
using Alx.Repo.Contracts.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Alx.Repo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {

            var item = await _mediator.Send(new GetItemQuery(id));
            if (item == null) return NotFound();
            return Ok(item);

        }

        [HttpGet()]
        public async Task<ActionResult> GetItems()
        {

            var items = await _mediator.Send(new ListItemsQuery());
            return Ok(items);

        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto createItem)
        {
            var item = await _mediator.Send(new CreateItemCommand(createItem));

            if (item == null || item.Id <= 0)
            {
                return NoContent();
            }

            return CreatedAtAction(nameof(CreateItem), new { id = item.Id }, item);

        }

        [HttpPut]
        public async Task<IActionResult> EditItem([FromBody] ItemDto editItem, int id)
        {
            try
            {
                var item = await _mediator.Send(new EditItemCommand(editItem, id));
                return Ok(item);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                await _mediator.Send(new DeleteItemCommand(id));
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
            

        }
    }
}
