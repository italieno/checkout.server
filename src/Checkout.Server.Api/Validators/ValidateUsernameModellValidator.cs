using Checkout.Server.Core.Models.Api.Request;
using FluentValidation;

namespace Checkout.Server.Api.Validators
{
    public class ShoppingCartItemInputModellValidator : AbstractValidator<ShoppingCartItemInputModel>
    {
        public ShoppingCartItemInputModellValidator()
        {
            RuleFor(input => input.What)
                .NotEmpty()
                .WithMessage("item identifier cannot be null or empty");

            RuleFor(input => input.HowMany)
                .NotEmpty()
                .WithMessage("number of items cannot be null or empty")
                .GreaterThanOrEqualTo(0)
                .WithMessage("number of items should be greate or equal than 0");
        }
    }
}