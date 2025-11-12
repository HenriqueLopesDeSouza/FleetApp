using FleetApp.Application.Abstractions;
using FleetApp.Core.Domain;

namespace FleetApp.Application.Services;
/// <summary>
/// Application service responsible for vehicle operations (create, update color, query and list).
/// </summary>
public sealed class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repo;
    public VehicleService(IVehicleRepository repo) => _repo = repo;


    public async Task AddAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        if (await _repo.ExistsAsync(vehicle.Chassis.Id, ct))
            throw new InvalidOperationException($"Vehicle with chassis '{vehicle.Chassis.Id}' already exists.");
        await _repo.AddAsync(vehicle, ct);
    }


    public async Task ChangeColorAsync(string chassisId, string color, CancellationToken ct = default)
    {
        var v = await _repo.GetByChassisIdAsync(chassisId, ct)
        ?? throw new KeyNotFoundException($"Vehicle '{chassisId}' not found.");
        v.ChangeColor(color);
        await _repo.UpdateAsync(v, ct);
    }


    public Task<Vehicle?> GetAsync(string chassisId, CancellationToken ct = default)
    => _repo.GetByChassisIdAsync(chassisId, ct);


    public Task<IReadOnlyList<Vehicle>> ListAsync(CancellationToken ct = default)
    => _repo.ListAsync(ct);
}