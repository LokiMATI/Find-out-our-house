namespace FindOutOurHouse.DAL.Coordinates;

/// <summary>
/// Координаты.
/// </summary>
public class Coordinate
{
    /// <summary>
    /// Широта.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Создание на основе первичных данных.
    /// </summary>
    /// <param name="latitude">Широта.</param>
    /// <param name="longitude">Долгота.</param>
    public Coordinate(
        double latitude,
        double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
