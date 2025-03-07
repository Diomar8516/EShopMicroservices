namespace Basket.API.Data;
public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancelationToken = default)
    {
        var cachebasket = await cache.GetStringAsync(userName, cancelationToken);
        if (!string.IsNullOrEmpty(cachebasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachebasket);
        
        var basket = await repository.GetBasket(userName, cancelationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancelationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {        
        await repository.StoreBasket(basket, cancellationToken);

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }

}
