namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        //Update Order Entity from comman object
        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        //Save to database
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        //return result
        return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var shippingAddressDto = orderDto.ShippingAddress;
        var billingAddressDto = orderDto.BillingAddress;
        var paymentDto = orderDto.Payment;

        var updateShippingAddress = Address.Of(shippingAddressDto.FirstName, shippingAddressDto.LastName, shippingAddressDto.EmailAddress,
                                         shippingAddressDto.AddressLine, shippingAddressDto.Country, shippingAddressDto.State,
                                         shippingAddressDto.ZipCode);
        var updateBillingAddress = Address.Of(billingAddressDto.FirstName, billingAddressDto.LastName, billingAddressDto.EmailAddress,
                                        billingAddressDto.AddressLine, billingAddressDto.Country, billingAddressDto.State,
                                        billingAddressDto.ZipCode);
        var updatePayment = Payment.Of(paymentDto.CardName, paymentDto.CardNumber, paymentDto.Expiration, paymentDto.Cvv, paymentDto.PaymentMethod);

        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updateShippingAddress,
            billingAddress: updateBillingAddress,
            payment: updatePayment,
            status: orderDto.Status);
    }
}
