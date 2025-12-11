
using Catalog.Api.Products.UpdateProduct;

namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommandRequest(Guid Id);
    public record DeleteProductCommandResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{Id}", async (Guid Id, ISender sender) =>
            {
                var result = sender.Send(new DeleteProductCommand(Id));

                var response = result.Adapt<DeleteProductCommandResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductCommandResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDescription("Delete Product")
            .WithSummary("Delete Product"); ;
        }
    }
}
