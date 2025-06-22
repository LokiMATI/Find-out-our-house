using FindOutOurHouse.DAL.Coordinates;
using FindOutOurHouse.DAL.Images;

namespace FindOutOurHouse.API.DTO.Places;

/// <summary>
/// DTO для обновления места.
/// </summary>
/// <param name="Title">Название.</param>
/// <param name="Description">Описание.</param>
/// <param name="Images">Изображения.</param>
/// <param name="Coordinate">Координаты.</param>
public record PlaceUpdateDto(
    string? Title = null,
    string? Description = null,
    List<byte[]>? Images = null,
    Coordinate? Coordinate = null);