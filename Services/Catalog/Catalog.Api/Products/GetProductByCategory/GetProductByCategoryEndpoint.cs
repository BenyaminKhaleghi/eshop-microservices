
using Catalog.Api.Products.GetProductById;
using Mapster;

namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryRequest(string category);
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("products/category/{category}", async (string category ,ISender sender) => 
            {
                var result = await sender.Send(new GetProductByCategoryRequest(category));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Product By Category")
            .WithSummary("Get Product By Category");
        }
    }
}
