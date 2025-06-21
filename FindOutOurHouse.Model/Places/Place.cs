using FindOutOurHouse.DAL.Coordinates;

namespace FindOutOurHouse.DAL.Places;

/// <summary>
/// Место.
/// </summary>
public class Place
{
    private string _title;
    private string? _description;

    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Title
    {
        get => _title;
        set
        { 
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be empty or whitespace.", nameof(value));
            
            _title = value;
        }
    }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description
    {
        get => _description;
        set
        {
            if (value is not null && value.Trim().Length == 0)
                throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));
            
            _description = value;
        }
    }

    /// <summary>
    /// Изображения.
    /// </summary>
    public List<Guid> Images { get; set; }
    
    /// <summary>
    /// Координаты.
    /// </summary>
    public Coordinate Coordinate { get; set; }

    /// <summary>
    /// Создание на основе первичных данных.
    /// </summary>
    /// <param name="title">Название.</param>
    /// <param name="description">Описание.</param>
    /// <param name="coordinate">Координаты.</param>
    public Place(
        string title,
        string? description,
        Coordinate coordinate)
        : this(
            title,
            coordinate)
    {
        Id = Guid.NewGuid();
        
        if (description is not null)
            Description = description;
    }

    private Place(
        string title,
        Coordinate coordinate)
    {
        Title = title;
        Coordinate = coordinate;
        Images = new();
    }
}