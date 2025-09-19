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
            if (items == null) return NotFound();
            return Ok(items);

        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateItemCommand item)
        {
            var itemId = await _mediator.Send(item);

            if(itemId  <= 0)

                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = string.Format("/items/{0}", itemId) });
        }
    }
}
