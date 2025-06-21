using FindOutOurHouse.DAL.Coordinates;
using FindOutOurHouse.DAL.Places;

namespace FindOutOurHouse.DAL.Repositories;

/// <summary>
/// Репозиторий мест.
/// </summary>
public interface IPlaceRepository
{
    /// <summary>
    /// Получить место.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="includeTypes">Включённые типы.</param>
    /// <returns>Место.</returns>
    public Task<Place?> GetAsync(
        Guid id,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None);

    /// <summary>
    /// Получить список мест.
    /// </summary>
    /// <param name="title">Название.</param>
    /// <param name="description">Описание.</param>
    /// <param name="includeTypes">Включённые типы.</param>
    /// <returns>Список мест.</returns>
    public Task<IEnumerable<Place>> GetListAsync(
        string? title = null,
        string? description = null,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None);

    /// <summary>
    /// Получить список мест в пределах окружности.
    /// </summary>
    /// <param name="center">Центр окружности.</param>
    /// <param name="radius">Радиус.</param>
    /// <param name="includeTypes">Включённые типы.</param>
    /// <returns>Список мест в пределах окружности.</returns>
    public Task<IEnumerable<Place>> GetListWithinRadiusAsync(
        Coordinate center,
        double radius,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None);

    /// <summary>
    /// Создать новое место.
    /// </summary>
    /// <param name="title">Название.</param>
    /// <param name="description">Описание.</param>
    /// <param name="coordinate">Координаты.</param>
    /// <returns>Созданное место.</returns>
    public Task<Place> CreateAsync(
        string title,
        string? description,
        Coordinate coordinate);

    /// <summary>
    /// Обновить место.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="title">Название.</param>
    /// <param name="description">Описание.</param>
    /// <param name="images">Идентификаторы изображений.</param>
    /// <param name="coordinate">Координаты.</param>
    /// <returns>Обновлённая место.</returns>
    public Task<Place> UpdateAsync(Guid id,
        string? title,
        string? description,
        List<Guid>? images,
        Coordinate? coordinate);
    
    /// <summary>
    /// Удалить место.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    public Task DeleteAsync(
        Guid id);
}
