using FleetApp.Core.Domain;
namespace FleetApp.Application.Abstractions;

/// <summary>
/// Defines the contract for vehicle persistence operations.
/// </summary>
public interface IVehicleRepository
{
    Task<bool> ExistsAsync(string chassisId, CancellationToken ct = default);
    Task AddAsync(Vehicle vehicle, CancellationToken ct = default);
    Task<Vehicle?> GetByChassisIdAsync(string chassisId, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> ListAsync(CancellationToken ct = default);
    Task UpdateAsync(Vehicle vehicle, CancellationToken ct = default);
}