using Accommodations.Domain.Constants;
using FluentValidation.TestHelper;
using Xunit;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations.Tests
{
    public class GetAllAccommodationsQueryValidatorTests
    {
        [Fact]
        public void ValidatorForValidQuery_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var query = new GetAllAccommodationsQuery
            {
                SearchPhrase = "beautiful",
                PageSize = 10,
                PageNumber = 1,
                SortBy = "Name",
                sortDirection = SortDirection.Ascending
            };

            var validator = new GetAllAccommodationsQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidatorForInvalidPageSize_ShouldHaveValidationError()
        {
            // Arrange
            var query = new GetAllAccommodationsQuery
            {
                SearchPhrase = "beautiful",
                PageSize = 20, // invalid page size
                PageNumber = 1,
                SortBy = "Name",
                sortDirection = SortDirection.Ascending
            };

            var validator = new GetAllAccommodationsQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                  .WithErrorMessage("Page size must be in [5,10,15,30]");
        }

        [Fact]
        public void ValidatorForInvalidPageNumber_ShouldHaveValidationError()
        {
            // Arrange
            var query = new GetAllAccommodationsQuery
            {
                SearchPhrase = "beautiful",
                PageSize = 10,
                PageNumber = 0, // invalid page number
                SortBy = "Name",
                sortDirection = SortDirection.Ascending
            };

            var validator = new GetAllAccommodationsQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Fact]
        public void ValidatorForInvalidSortBy_ShouldHaveValidationError()
        {
            // Arrange
            var query = new GetAllAccommodationsQuery
            {
                SearchPhrase = "beautiful",
                PageSize = 10,
                PageNumber = 1,
                SortBy = "InvalidColumn", // invalid sort by column
                sortDirection = SortDirection.Ascending
            };

            var validator = new GetAllAccommodationsQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SortBy)
                  .WithErrorMessage("Sort by is optional, or must be in [Name,Description,Type,City]");
        }

        [Fact]
        public void ValidatorForNullSortBy_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var query = new GetAllAccommodationsQuery
            {
                SearchPhrase = "beautiful",
                PageSize = 10,
                PageNumber = 1,
                SortBy = null,
                sortDirection = SortDirection.Ascending
            };

            var validator = new GetAllAccommodationsQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.SortBy);
        }
    }
}
