namespace FindOutOurHouse.DAL.Images;

/// <summary>
/// Изображение.
/// </summary>
public class Image
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Данные.
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    /// Создание на основе первичных данных.
    /// </summary>
    /// <param name="data">Данные.</param>
    public Image(byte[] data)
    {
        Id = Guid.NewGuid();
        Data = data;
    }
}