namespace FindOutOurHouse.DAL.Places;

/// <summary>
/// Типы для включения в запросе для места.
/// </summary>
[Flags]
public enum PlaceQueryIncludeTypes
{
    /// <summary>
    /// Ничего.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Изображения.
    /// </summary>
    Images = 1,
    
    /// <summary>
    /// Всё.
    /// </summary>
    All = ~None
}