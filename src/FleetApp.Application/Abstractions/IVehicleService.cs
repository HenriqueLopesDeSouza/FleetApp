using FleetApp.Core.Domain;

namespace FleetApp.Application.Abstractions;

/// <summary>
/// Application service for vehicle operations (create, edit color, query and list).
/// </summary>
public interface IVehicleService
{
    Task AddAsync(Vehicle vehicle, CancellationToken ct = default);

    /// <summary>Changes the color of an existing vehicle identified by its chassis ID.</summary>
    Task ChangeColorAsync(string chassisId, string color, CancellationToken ct = default);

    /// <summary>Gets a vehicle by chassis ID. Returns <c>null</c> when not found.</summary>
    Task<Vehicle?> GetAsync(string chassisId, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> ListAsync(CancellationToken ct = default);
}