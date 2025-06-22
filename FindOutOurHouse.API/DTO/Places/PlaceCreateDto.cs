using FindOutOurHouse.DAL.Coordinates;

namespace FindOutOurHouse.API.DTO.Places;

/// <summary>
/// DTO для создания места.
/// </summary>
/// <param name="Title">Название.</param>
/// <param name="Description">Описание.</param>
/// <param name="Coordinate">Координаты.</param>
public record PlaceCreateDto(
    string Title,
    string? Description,
    Coordinate Coordinate);