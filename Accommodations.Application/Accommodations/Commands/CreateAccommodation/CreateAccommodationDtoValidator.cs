using Accommodations.Domain.Entities;
using FluentValidation;

namespace Accommodations.App.Accommodations.Commands.CreateAccommodation
{
    public class CreateAccommodationCommandValidator : AbstractValidator<CreateAccommodationCommand>
    {
        public CreateAccommodationCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty()
                .Length(3, 1000);

            RuleFor(dto => dto.Type)
                .NotEmpty()
                .Must(BeAValidAccommodationType)
                .WithMessage($"Accommodation Type is not valid. Valid types are: {GetValidAccommodationTypes()}.");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .When(dto => !string.IsNullOrEmpty(dto.ContactEmail));

            RuleFor(dto => dto.ContactNumber)
                .Matches(@"^\+?\d{8,20}$")
                .When(dto => !string.IsNullOrEmpty(dto.ContactNumber))
                .WithMessage("Contact Number must be a valid international phone number.");

            RuleFor(dto => dto.City)
               .Length(1, 100)
               .When(dto => !string.IsNullOrEmpty(dto.City));

            RuleFor(dto => dto.Street)
                .Length(1, 100)
                .When(dto => !string.IsNullOrEmpty(dto.Street));

            RuleFor(dto => dto.PostalCode)
                .Length(1, 50)
                .When(dto => !string.IsNullOrEmpty(dto.PostalCode));
        }


        private bool BeAValidAccommodationType(string type)
        {
            return Enum.TryParse(typeof(AccommodationType), type, true, out _);
        }

        private string GetValidAccommodationTypes()
        {
            var enumValues = Enum.GetNames(typeof(AccommodationType));
            return string.Join(", ", enumValues);
        }
    }
}
