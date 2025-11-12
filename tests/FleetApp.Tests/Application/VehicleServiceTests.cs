using FleetApp.Application.Abstractions;
using FleetApp.Application.Services;
using FleetApp.Core.Domain;
using FluentAssertions;
using Moq;

namespace FleetApp.Tests.Application;

public class VehicleServiceTests
{
    private readonly Mock<IVehicleRepository> _repo = new();
    private readonly IVehicleService _sut;

    public VehicleServiceTests()
    {
        _sut = new VehicleService(_repo.Object);
    }

    [Fact]
    public async Task AddAsync_adds_when_not_exists()
    {
        var v = VehicleFactory.Create(VehicleType.Truck, new Chassis("TT", 9u), "Gray");
        _repo.Setup(r => r.ExistsAsync(v.Chassis.Id, default)).ReturnsAsync(false);

        await _sut.AddAsync(v); 

        _repo.Verify(r => r.AddAsync(v, default), Times.Once);
    }

    [Fact]
    public async Task AddAsync_throws_when_vehicle_already_exists()
    {
        var v = VehicleFactory.Create(VehicleType.Car, new Chassis("ABC", 1u), "Red");
        _repo.Setup(r => r.ExistsAsync(v.Chassis.Id, default)).ReturnsAsync(true);

        var act = async () => await _sut.AddAsync(v);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Vehicle with chassis '{v.Chassis.Id}' already exists.");
    }

    [Fact]
    public async Task ChangeColorAsync_throws_when_not_found()
    {
        _repo.Setup(r => r.GetByChassisIdAsync("X-1", default))
             .ReturnsAsync((Vehicle?)null);

        var act = () => _sut.ChangeColorAsync("X-1", "Blue");

        await act.Should().ThrowAsync<KeyNotFoundException>();
        _repo.Verify(r => r.UpdateAsync(It.IsAny<Vehicle>(), default), Times.Never);
    }

    [Fact]
    public async Task ChangeColorAsync_updates_and_persists()
    {
        var existing = VehicleFactory.Create(VehicleType.Bus, new Chassis("BUS", 42u), "Yellow");
        _repo.Setup(r => r.GetByChassisIdAsync(existing.Chassis.Id, default)).ReturnsAsync(existing);

        await _sut.ChangeColorAsync(existing.Chassis.Id, "Blue");

        existing.Color.Should().Be("Blue");
        _repo.Verify(r => r.UpdateAsync(existing, default), Times.Once);
    }

    [Fact]
    public async Task Get_and_List_delegate_to_repository()
    {
        var v = VehicleFactory.Create(VehicleType.Car, new Chassis("A", 1u), "Red");
        _repo.Setup(r => r.GetByChassisIdAsync(v.Chassis.Id, default)).ReturnsAsync(v);
        _repo.Setup(r => r.ListAsync(default)).ReturnsAsync(new List<Vehicle> { v });

        (await _sut.GetAsync(v.Chassis.Id)).Should().BeSameAs(v);
        (await _sut.ListAsync()).Should().ContainSingle().Which.Should().BeSameAs(v);
    }
}
