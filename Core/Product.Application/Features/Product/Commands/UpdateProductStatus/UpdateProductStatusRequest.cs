using MediatR;
using Product.Domain.Common.Enums;

namespace Product.Application.Features.Product.Commands.UpdateProductStatus
{
    public class UpdateProductStatusRequest :IRequest<bool>
    {
        public Guid Id { get; set; }
        public RecordStatusEnum Status { get; set; }
    }
}
