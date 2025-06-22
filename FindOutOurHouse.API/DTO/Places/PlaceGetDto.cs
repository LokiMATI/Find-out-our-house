using FindOutOurHouse.DAL.Coordinates;

namespace FindOutOurHouse.API.DTO.Places;

/// <summary>
/// DTO для получения мест в радиусе.
/// </summary>
/// <param name="Coordinate">Центр окружности.</param>
/// <param name="Radius">Радиус.</param>
public record PlaceGetDto(
    Coordinate Coordinate,
    double Radius);