namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    public class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler.Hnadle called with {@query}", query);
            
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            
            return new GetProductsResult(products);
        }
    }
}
