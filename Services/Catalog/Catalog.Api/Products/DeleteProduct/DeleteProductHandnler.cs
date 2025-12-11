
using Catalog.Api.Products.UpdateProduct;
using JasperFx.Events.Daemon;

namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSussess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(Command => Command.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }
    public class DeleteProductCommandHandnler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {

            session.Delete<Product>(command.Id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
