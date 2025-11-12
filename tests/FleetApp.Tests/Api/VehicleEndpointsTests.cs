using System.Net;
using System.Net.Http.Json;
using FleetApp.Api.Dtos;
using FleetApp.Application.Abstractions;
using FleetApp.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FleetApp.Tests.Api;

public class VehicleEndpointsTests
{
    private readonly HttpClient _client;

    public VehicleEndpointsTests()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(IVehicleRepository));
                    services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();
                });
            });

        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_returns_201_and_location()
    {
        var req = new CreateVehicleRequest("ABC", 1u, "Car", "Red");

        var resp = await _client.PostAsJsonAsync("/v1/vehicles", req);

        resp.StatusCode.Should().Be(HttpStatusCode.Created);
        resp.Headers.Location.Should().NotBeNull();
        resp.Headers.Location!.AbsolutePath.Should().Be("/v1/vehicles/ABC-1");

        var body = await resp.Content.ReadFromJsonAsync<VehicleResponse>();
        body!.ChassisId.Should().Be("ABC-1");
        body.Type.Should().Be("Car");
        body.NumberOfPassengers.Should().Be(4);
        body.Color.Should().Be("Red");
    }

    [Fact]
    public async Task Create_duplicate_returns_409_conflict_problem()
    {
        var req = new CreateVehicleRequest("ABC", 1u, "Truck", "Gray");

        (await _client.PostAsJsonAsync("/v1/vehicles", req)).EnsureSuccessStatusCode();
        var resp2 = await _client.PostAsJsonAsync("/v1/vehicles", req);

        resp2.StatusCode.Should().Be(HttpStatusCode.Conflict);
        var problem = await resp2.Content.ReadAsStringAsync();
        problem.Should().Contain("already exists");
    }

    [Fact]
    public async Task Patch_color_updates_and_returns_200()
    {
        var create = new CreateVehicleRequest("BUS", 42u, "Bus", "Yellow");
        (await _client.PostAsJsonAsync("/v1/vehicles", create)).EnsureSuccessStatusCode();

        var patch = new ChangeColorRequest("Blue");
        var resp = await _client.PatchAsJsonAsync("/v1/vehicles/BUS-42/color", patch);

        resp.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await resp.Content.ReadFromJsonAsync<VehicleResponse>();
        body!.Color.Should().Be("Blue");
        body.Type.Should().Be("Bus");
        body.NumberOfPassengers.Should().Be(42);
    }

    [Fact]
    public async Task List_returns_200_with_created_item()
    {
        await _client.PostAsJsonAsync("/v1/vehicles", new CreateVehicleRequest("AAA", 1u, "Car", "Red"));

        var resp = await _client.GetAsync("/v1/vehicles");
        resp.StatusCode.Should().Be(HttpStatusCode.OK);

        var items = await resp.Content.ReadFromJsonAsync<IEnumerable<VehicleResponse>>();
        items!.Should().ContainSingle(i => i.ChassisId == "AAA-1" && i.Type == "Car");
    }

    [Fact]
    public async Task Get_returns_404_when_not_found()
    {
        var resp = await _client.GetAsync("/v1/vehicles/NOPE-9");
        resp.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
