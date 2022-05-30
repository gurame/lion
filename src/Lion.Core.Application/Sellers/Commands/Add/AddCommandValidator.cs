using FluentValidation;

namespace Lion.Core.Application.Sellers.Commands.Add;

public class AddCommandValidator : AbstractValidator<AddCommand>
{
    public AddCommandValidator()
    {
        RuleFor(v => v.TaxId).NotEmpty();
        RuleFor(v => v.Name).NotEmpty();
    }
}
