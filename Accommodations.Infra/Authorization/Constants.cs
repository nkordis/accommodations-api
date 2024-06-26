
namespace Accommodations.Infra.Authorization
{
    public static class PolicyNames
    {
        public const string HasNationality = "HasNationality";
        public const string AtLeast18 = "AtLeast18";
        public const string CreatedAtLeast2Accommodations = "CreatedAtLeast2Accommodations";
    }

    public static class AppClaimTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";
    }
}
