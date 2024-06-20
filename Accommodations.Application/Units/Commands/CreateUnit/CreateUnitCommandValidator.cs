using Accommodations.Domain.Entities;
using FluentValidation;

namespace Accommodations.App.Units.Commands.CreateUnit
{
    public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
    {
        private const int MaxPrice = 1000000000;
        private const int MaxCapacity = 100;       

        public CreateUnitCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty()
                .Length(3, 1000);

            RuleFor(dto => dto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .LessThanOrEqualTo(MaxPrice).WithMessage($"Price must be less than or equal to {MaxPrice}.")
                .Must(HaveValidDecimalPlaces).WithMessage("Price must have no more than 2 decimal places.");

            RuleFor(dto => dto.BillingPeriod)
                .NotEmpty()
                .Must(BeAValidBillingPeriodType)
                .WithMessage($"{nameof(Unit.BillingPeriod)} Type is not valid. Valid types are: {GetValidBillingPeriodTypes()}.");

            RuleFor(dto => dto.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than zero.")
            .LessThanOrEqualTo(MaxCapacity).WithMessage($"Capacity must be less than or equal to {MaxCapacity}.");

            RuleFor(dto => dto.Type)
                .NotEmpty()
                .Must(BeAValidUnitType)
                .WithMessage($"{nameof(Unit.Type)} Type is not valid. Valid types are: {GetValidUnitTypes()}.");

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

        private bool BeAValidBillingPeriodType(string type)
        {
            return Enum.TryParse(typeof(BillingPeriod), type, true, out _);
        }

        private string GetValidBillingPeriodTypes()
        {
            var enumValues = Enum.GetNames(typeof(BillingPeriod));
            return string.Join(", ", enumValues);
        }

        private bool HaveValidDecimalPlaces(decimal price)
        {
            var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(price)[3])[2];
            return decimalPlaces <= 2;
        }

        private string GetValidUnitTypes()
        {
            var enumValues = Enum.GetNames(typeof(UnitType));
            return string.Join(", ", enumValues);
        }

        private bool BeAValidUnitType(string type)
        {
            return Enum.TryParse(typeof(UnitType), type, true, out _);
        }
    }
}
