using FluentValidation;

namespace Application.Commands
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(b => b.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(b => b.LastName).NotEmpty().MaximumLength(100);
            RuleFor(b => b.DateOfBirth).NotEmpty();
            RuleFor(b => b.Gender).NotEmpty();
            RuleFor(b => b.Email).NotEmpty();
            RuleFor(b => b.PhoneNumber).NotEmpty();
            RuleFor(b => b.Address).NotEmpty();
        }
    }
}