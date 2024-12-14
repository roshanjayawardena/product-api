using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Features.Product.Commands.DeleteProduct;
using Product.Application.Features.Product.Commands.UpdateProduct;
using Product.Application.Features.Product.Commands.UpdateProductStatus;
using Product.Application.Features.Product.Queries.GetAllProducts;
using Product.Application.Features.Product.Queries.GetProductDetail;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllProductRequest());
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetProductDetailRequest(id));
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Post([FromBody] CreateProductRequest item)
        {
            var response = await _mediator.Send(item);
            return CreatedAtAction(nameof(Post), new { id = response });
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Put([FromBody] UpdateProductRequest item)
        {
            await _mediator.Send(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteProductRequest(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("UpdateStatus")]
        public async Task<ActionResult> Put([FromBody] UpdateProductStatusRequest item)
        {
            await _mediator.Send(item);
            return NoContent();
        }
    }
}
