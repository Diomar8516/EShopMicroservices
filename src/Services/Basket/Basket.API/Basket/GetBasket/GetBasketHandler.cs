using Basket.API.Models;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);
public class GetBasketQueryHandler
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // TODO: Get basket from database
        //var basket = await _repository.Getbasket(request.UserName);

        return new GetBasketResult(new ShoppingCart("swn"));
    }
}
