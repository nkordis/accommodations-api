using Accommodations.Domain.Entities;
using FluentValidation;

namespace Accommodations.App.Accommodations.Commands.UpdateAccommodation
{
    public class UpdateAccommodationCommandValidator : AbstractValidator<UpdateAccommodationCommand>
    {
        public UpdateAccommodationCommandValidator()
        {
            RuleFor(x => x)
            .Must(HaveAtLeastOneFieldPopulated)
            .WithMessage("At least one field must be populated.");

            RuleFor(dto => dto.Name)
                .NotEmpty()
                .Length(3, 100)
                .When(dto => dto.Name != null);

            RuleFor(dto => dto.Description)
                .NotEmpty()
                .Length(3, 1000)
                .When(dto => dto.Description != null);

            RuleFor(dto => dto.Type)
                .NotEmpty()
                .Must(BeAValidAccommodationType!)
                .WithMessage($"Accommodation Type is not valid. Valid types are: {GetValidAccommodationTypes()}.")
                .When(dto => dto.Type != null);

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .When(dto => !string.IsNullOrEmpty(dto.ContactEmail));

            RuleFor(dto => dto.ContactNumber)
                .Matches(@"^\+?\d{8,20}$")
                .When(dto => !string.IsNullOrEmpty(dto.ContactNumber))
                .WithMessage("Contact Number must be a valid international phone number.");
        }

        private bool HaveAtLeastOneFieldPopulated(UpdateAccommodationCommand command)
        {
            return !string.IsNullOrEmpty(command.Name) ||
                   !string.IsNullOrEmpty(command.Description) ||
                   !string.IsNullOrEmpty(command.Type) ||
                   command.HasInstantBooking.HasValue ||
                   !string.IsNullOrEmpty(command.ContactEmail) ||
                   !string.IsNullOrEmpty(command.ContactNumber);
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
