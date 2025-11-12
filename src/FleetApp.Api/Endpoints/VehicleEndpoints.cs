using FluentValidation;
using FleetApp.Api.Dtos;
using FleetApp.Api.Extensions; 
using FleetApp.Application.Abstractions;
using FleetApp.Core.Domain;

namespace FleetApp.Api.Endpoints;

public static class VehicleEndpoints
{
    public static IEndpointRouteBuilder MapVehicleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/vehicles").WithTags("Vehicles");

        group.MapPost("",
            async Task<IResult> (
                CreateVehicleRequest req,
                IValidator<CreateVehicleRequest> validator,
                IVehicleService svc,
                CancellationToken ct) =>
            {
                var vr = await validator.ValidateAsync(req, ct);
                if (!vr.IsValid) return Results.ValidationProblem(vr.ToProblem());

                if (!Enum.TryParse<VehicleType>(req.Type, true, out var vt))
                    return Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        ["Type"] = new[] { "Type must be Bus, Truck or Car." }
                    });

                var vehicle = VehicleFactory.Create(vt, new Chassis(req.ChassisSeries, req.ChassisNumber), req.Color);
                await svc.AddAsync(vehicle, ct);

                var resp = new VehicleResponse(vehicle.Chassis.Id, vehicle.Type.ToString(), vehicle.NumberOfPassengers, vehicle.Color);
                return Results.CreatedAtRoute("GetVehicle", new { chassisId = resp.ChassisId }, resp);
            })
            .WithName("CreateVehicle")
            .WithSummary("Create a new vehicle")
            .Produces<VehicleResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesValidationProblem()
            .WithOpenApi();

        group.MapPatch("/{chassisId}/color",
            async Task<IResult> (
                string chassisId,
                ChangeColorRequest req,
                IValidator<ChangeColorRequest> validator,
                IVehicleService svc,
                CancellationToken ct) =>
            {
                var vr = await validator.ValidateAsync(req, ct);
                if (!vr.IsValid) return Results.ValidationProblem(vr.ToProblem());

                await svc.ChangeColorAsync(chassisId, req.Color, ct);

                var v = await svc.GetAsync(chassisId, ct);
                if (v is null) return Results.NotFound();

                var resp = new VehicleResponse(v.Chassis.Id, v.Type.ToString(), v.NumberOfPassengers, v.Color);
                return Results.Ok(resp);
            })
            .WithName("ChangeVehicleColor")
            .WithSummary("Change vehicle color")
            .Produces<VehicleResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi();

        group.MapGet("",
            async Task<IResult> (IVehicleService svc, CancellationToken ct) =>
            {
                var list = await svc.ListAsync(ct);
                var resp = list.Select(v => new VehicleResponse(v.Chassis.Id, v.Type.ToString(), v.NumberOfPassengers, v.Color));
                return Results.Ok(resp);
            })
            .WithName("ListVehicles")
            .WithSummary("List all vehicles")
            .Produces<IEnumerable<VehicleResponse>>(StatusCodes.Status200OK)
            .WithOpenApi();

        group.MapGet("/{chassisId}",
            async Task<IResult> (string chassisId, IVehicleService svc, CancellationToken ct) =>
            {
                var v = await svc.GetAsync(chassisId, ct);
                return v is null
                    ? Results.NotFound()
                    : Results.Ok(new VehicleResponse(v.Chassis.Id, v.Type.ToString(), v.NumberOfPassengers, v.Color));
            })
            .WithName("GetVehicle")
            .WithSummary("Get vehicle by chassisId")
            .Produces<VehicleResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi();

        return app;
    }
}
