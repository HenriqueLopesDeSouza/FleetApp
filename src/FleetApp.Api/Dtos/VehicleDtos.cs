using System.Text.Json.Serialization;

namespace FleetApp.Api.Dtos;

/// <summary>
/// Represents a request to create a new vehicle.
/// </summary>
public sealed record CreateVehicleRequest(

    /// <summary>
    /// The alphanumeric part of the chassis identifier (e.g., "ABC123").
    /// </summary>
    [property: JsonRequired] string ChassisSeries,

    /// <summary>
    /// The numeric part of the chassis identifier. 
    /// Must be provided and greater than zero.
    /// </summary>
    [property: JsonRequired] uint ChassisNumber,

    /// <summary>
    /// The vehicle type (Car, Truck, or Bus).
    /// </summary>
    [property: JsonRequired] string Type,

    /// <summary>
    /// The initial color of the vehicle.
    /// </summary>
    [property: JsonRequired] string Color
);

/// <summary>
/// Request payload for changing the color of an existing vehicle.
/// </summary>
public sealed record ChangeColorRequest(
    /// <summary>New color for the vehicle.</summary>
    string Color);


/// <summary>
/// Response object representing vehicle data returned by the API.
/// </summary>
public sealed record VehicleResponse(
    /// <summary>Unique chassis identifier of the vehicle.</summary>
    string ChassisId,

    /// <summary>Vehicle type (Car, Truck or Bus).</summary>
    string Type,

    /// <summary>Number of passengers supported by the vehicle.</summary>
    int NumberOfPassengers,

    /// <summary>Current color of the vehicle.</summary>
    string Color);
