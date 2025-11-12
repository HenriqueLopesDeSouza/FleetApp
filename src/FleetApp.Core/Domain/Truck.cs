namespace FleetApp.Core.Domain;
/// <summary>
/// Represents a truck in the fleet.
/// </summary>
public sealed class Truck : Vehicle
{
    public Truck(Chassis chassis, string color) : base(chassis, color) { }
    public override VehicleType Type => VehicleType.Truck;
    public override int NumberOfPassengers => 1;
}