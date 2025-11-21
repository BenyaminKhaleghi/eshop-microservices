using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out Tresponse>: IRequest<Tresponse>
        where Tresponse : notnull
    {
    }
}
