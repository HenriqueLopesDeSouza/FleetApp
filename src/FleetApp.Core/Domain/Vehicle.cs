namespace FleetApp.Core.Domain;
/// <summary>
/// Represents a generic vehicle within the fleet domain.
/// </summary>
public abstract class Vehicle
{
    protected Vehicle(Chassis chassis, string color)
    {
        ArgumentNullException.ThrowIfNull(chassis);
        ChangeColor(color);
        Chassis = chassis;
    }

    public Chassis Chassis { get; }
    public string Color { get; private set; } = string.Empty;
    public abstract VehicleType Type { get; }
    public abstract int NumberOfPassengers { get; }

    public void ChangeColor(string newColor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newColor);
        Color = newColor.Trim();
    }
}