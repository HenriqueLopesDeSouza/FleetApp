namespace FleetApp.Core.Domain;
/// <summary>
/// Factory responsible for creating specific <see cref="Vehicle"/> instances based on <see cref="VehicleType"/>.
/// </summary>
public static class VehicleFactory
{
    public static Vehicle Create(VehicleType type, Chassis chassis, string color) => type switch
    {
        VehicleType.Bus => new Bus(chassis, color),
        VehicleType.Truck => new Truck(chassis, color),
        VehicleType.Car => new Car(chassis, color),
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}