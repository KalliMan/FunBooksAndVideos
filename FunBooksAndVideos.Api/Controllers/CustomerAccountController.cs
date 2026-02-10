using FunBooksAndVideos.Api.Models.Requests;
using FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;
using FunBooksAndVideos.Application.Features.Customer.Commands.DeleteCustomerAccount;
using FunBooksAndVideos.Application.Features.Customer.Commands.UpdateCustomerAccount;
using FunBooksAndVideos.Application.Features.Customer.Queries.GetAllCustomerAccounts;
using FunBooksAndVideos.Application.Features.Customer.Queries.GetCustomerAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        public CustomerAccountController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllCustomerAccountsQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation("Retrieved {Count} customer accounts", result.Count());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetCustomerAccountQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Customer account with ID {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Retrieved customer account with ID {Id}", id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerAccountRequest request)
        {
            var command = new CreateCustomerAccountCommand(request.Name, request.MembershipTypes);
            var id = await _mediator.Send(command);

            _logger.LogInformation("Created new customer account with ID {Id}", id);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerAccountRequest request)
        {
            var command = new UpdateCustomerAccountCommand (id, request.Name);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCustomerAccountCommand(id);
            await _mediator.Send(command);

            _logger.LogInformation("Deleted customer account with ID {Id}", id);
            return NoContent();
        }
    }
}
