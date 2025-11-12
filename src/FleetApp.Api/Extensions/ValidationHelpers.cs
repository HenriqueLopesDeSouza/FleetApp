namespace FleetApp.Api.Extensions;

internal static class ValidationHelpers
{
    public static Dictionary<string, string[]> ToProblem(this FluentValidation.Results.ValidationResult r) =>
        r.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());
}
