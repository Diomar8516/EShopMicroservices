namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //Create Order Entity from command object
        var order = CreateNewOrder(command.Order);

        //save to database
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddressDto = orderDto.ShippingAddress;
        var billingAddressDto = orderDto.BillingAddress;
        var paymentDto = orderDto.Payment;

        var shippingAddress = Address.Of(shippingAddressDto.FirstName, shippingAddressDto.LastName, shippingAddressDto.EmailAddress, 
                                         shippingAddressDto.AddressLine, shippingAddressDto.Country, shippingAddressDto.State, 
                                         shippingAddressDto.ZipCode);
        var billingAddress = Address.Of(billingAddressDto.FirstName, billingAddressDto.LastName, billingAddressDto.EmailAddress, 
                                        billingAddressDto.AddressLine, billingAddressDto.Country, billingAddressDto.State, 
                                        billingAddressDto.ZipCode);
        var payment = Payment.Of(paymentDto.CardName, paymentDto.CardNumber, paymentDto.Expiration, paymentDto.Cvv, paymentDto.PaymentMethod);

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: payment
            );

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }

        return newOrder;
    }
}
