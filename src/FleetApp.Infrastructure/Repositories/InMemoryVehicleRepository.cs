using System.Collections.Concurrent;
using FleetApp.Application.Abstractions;
using FleetApp.Core.Domain;

namespace FleetApp.Infrastructure.Repositories;

/// <summary>
/// Thread-safe in-memory implementation of <see cref="IVehicleRepository"/>.
/// Used as a lightweight persistence layer for demonstration and prototyping.
/// </summary>
public sealed class InMemoryVehicleRepository : IVehicleRepository
{
    private readonly ConcurrentDictionary<string, Vehicle> _db = new();


    public Task<bool> ExistsAsync(string chassisId, CancellationToken ct = default)
    => Task.FromResult(_db.ContainsKey(chassisId));


    public Task AddAsync(Vehicle vehicle, CancellationToken ct = default)
    { _db[vehicle.Chassis.Id] = vehicle; return Task.CompletedTask; }


    public Task<Vehicle?> GetByChassisIdAsync(string chassisId, CancellationToken ct = default)
    => Task.FromResult(_db.TryGetValue(chassisId, out var v) ? v : null);


    public Task<IReadOnlyList<Vehicle>> ListAsync(CancellationToken ct = default)
    => Task.FromResult<IReadOnlyList<Vehicle>>(_db.Values.OrderBy(v => v.Chassis.Id).ToList());


    public Task UpdateAsync(Vehicle vehicle, CancellationToken ct = default)
    { _db[vehicle.Chassis.Id] = vehicle; return Task.CompletedTask; }
}