
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id);
    public record ProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id,ISender sender) => 
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<ProductByIdResponse>();

                return Results.Ok(response);
            })
             .WithName("GetProductById")
            .Produces<ProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Product By Id")
            .WithSummary("Get Product By Id"); ;
        }
    }
}
