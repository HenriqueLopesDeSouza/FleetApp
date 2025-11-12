namespace FleetApp.Core.Domain;
/// <summary>
/// Represents a car in the fleet.
/// </summary>
public sealed class Car : Vehicle
{
    public Car(Chassis chassis, string color) : base(chassis, color) { }
    public override VehicleType Type => VehicleType.Car;
    public override int NumberOfPassengers => 4;
}