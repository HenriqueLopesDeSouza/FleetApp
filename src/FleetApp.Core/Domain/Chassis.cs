namespace FleetApp.Core.Domain;
/// <summary>
/// Represents a vehicle chassis (value object).
/// </summary>
public sealed record Chassis(string Series, uint Number)
{
    /// <summary>
    /// Unique chassis ID, normalized as SERIES-NUMBER (e.g., ABC-123).
    /// </summary>
    public string Id => $"{Series.Trim().ToUpperInvariant()}-{Number}";
}