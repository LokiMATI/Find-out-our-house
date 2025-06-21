using FindOutOurHouse.DAL.Images;

namespace FindOutOurHouse.DAL.Repositories;

/// <summary>
/// Репозиторий изображений.
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// Получить изображение.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Изображение.</returns>
    public Task<Image?> GetAsync(
        Guid id);
    
    /// <summary>
    /// Получить список изображений.
    /// </summary>
    /// <param name="ids">Список идентификаторов.</param>
    /// <returns>Список изображений.</returns>
    public Task<IEnumerable<Image>> GetImagesAsync(
        List<Guid> ids);
    
    /// <summary>
    /// Добавить изображение.
    /// </summary>
    /// <param name="data">Данные.</param>
    /// <returns>Добавленное изображение.</returns>
    public Task<Image> AddAsync(
        byte[] data);

    /// <summary>
    /// Добавить список изображений.
    /// </summary>
    /// <param name="files"></param>
    /// <returns>Список добавленных изображений.</returns>
    public Task<IEnumerable<Image>> AddListAsync(List<byte[]> files);
    
    /// <summary>
    /// Удалить изображение.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    public Task DeleteImageAsync(
        Guid id);
}