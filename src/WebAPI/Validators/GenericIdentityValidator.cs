namespace CleanMinimalApi.WebAPI.Validators;

using FluentValidation;

public class GenericIdentityValidator : AbstractValidator<Guid>
{
    public GenericIdentityValidator()
    {
        // TODO: adapt to ICP Guids
        _ = this.RuleFor(r => r).NotEqual(Guid.Empty).WithMessage("A valid Id was not supplied.");
    }
}
