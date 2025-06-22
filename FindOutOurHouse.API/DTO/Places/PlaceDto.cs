using FindOutOurHouse.DAL.Coordinates;
using FindOutOurHouse.DAL.Images;

namespace FindOutOurHouse.API.DTO.Places;

/// <summary>
/// DTO места.
/// </summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="Title">Название.</param>
/// <param name="Description">Описание.</param>
/// <param name="Images">Изображения.</param>
/// <param name="Coordinate">Координаты.</param>
public record PlaceDto(
    Guid Id,
    string Title,
    string? Description,
    IEnumerable<Image> Images,
    Coordinate Coordinate);