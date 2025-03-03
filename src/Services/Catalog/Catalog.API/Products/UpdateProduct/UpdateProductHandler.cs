using System.Security.Cryptography;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("El Id del producto es requerido");
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("El Nombre es requerido")
            .Length(2, 150).WithMessage("El Nombre debe estar entre 2 y 150 caracteres");
        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("El Precio debe ser mayor que 0.");
    }
}

internal class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.handle called with {@Command}", command);
        // Update Product entity from command object
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }


        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        // Update to database
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        //return UpdateProductResult result
        return new UpdateProductResult(true);
    }
}
