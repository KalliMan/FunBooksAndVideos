using FunBooksAndVideos.Api.Models.Requests;
using FunBooksAndVideos.Application.Features.Purchase.Commands.CreatePurchaseOrder;
using FunBooksAndVideos.Application.Features.Purchase.Queries.GetAllPurchaseOrders;
using FunBooksAndVideos.Application.Features.Purchase.Queries.GetPurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILogger<PurchaseOrderController> _logger;
        private readonly IMediator _mediator;
        public PurchaseOrderController(ILogger<PurchaseOrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllPurchaseOrdersQuery();
            var purchaseOrders = await _mediator.Send(query);

            return Ok(purchaseOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetPurchaseOrderQuery(id);
            var purchaseOrder = await _mediator.Send(query);

            if (purchaseOrder == null)
            {
                return NotFound();
            }
                
            return Ok(purchaseOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PurchaseOrderRequest request)
        {
            var command = new CreatePurchaseOrderCommand()
            {
                CustomerId = request.CustomerId,
                Items = request.Items.Select(i => new CreatePurchaseOrderItem
                {
                    ItemLineType = i.ItemLineType,
                    ProductId = i.ProductId,
                    MembershipType = i.MembershipType
                }).ToList()
            };

            var result = await _mediator.Send(command);

            _logger.LogInformation("Created new purchase order with ID {Id}", result.Id);
            return CreatedAtAction(nameof(Get), new { result.Id }, null);
        }
    }
}
