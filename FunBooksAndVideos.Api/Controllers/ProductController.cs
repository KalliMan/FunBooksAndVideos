using FunBooksAndVideos.Application.Features.OrderProduct.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);

            _logger.LogInformation("Retrieved {Count} products", products.Count());
            return Ok(products);
        }
    }
}
