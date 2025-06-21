using FindOutOurHouse.API.DTO.Places;
using FindOutOurHouse.DAL.Places;
using FindOutOurHouse.DAL.Repositories;

namespace FindOutOurHouse.API.AppServices;

/// <summary>
/// Сервисы для работы с местами.
/// </summary>
public class PlaceService(IPlaceRepository repository)
{
    public async Task<PlaceDto> GetPlaceAsync(
        Guid id)
    {
        var place = await repository.GetAsync(id);
        
        if (place is null)
            throw new KeyNotFoundException("Place not found.");
        
        return MapPlaceDto(place);
    }

    public async Task<IEnumerable<PlaceDto>> GetPlacesAsync(
        string? title = null,
        string? description = null)
    {
        var query = await repository.GetListAsync(
            title,
            description);
        
        return query.Select(MapPlaceDto);
    }

    public async Task<IEnumerable<PlaceDto>> GetPlacesAsync(
        PlaceGetDto input)
    {
        var places = await repository.GetListWithinRadiusAsync(
            input.Coordinate,
            input.Radius);
        
        return places.Select(MapPlaceDto);
    }

    public async Task<PlaceDto> CreatePlaceAsync(
        PlaceCreateDto input)
    {
        var place = await repository.CreateAsync(
            input.Title,
            input.Description,
            input.Coordinate);
        
        return MapPlaceDto(place);
    }

    public async Task<PlaceDto> UpdatePlaceAsync(
        Guid id,
        PlaceUpdateDto input)
    {
        var place = await repository.UpdateAsync(
            id,
            input.Title,
            input.Description,
            input.Images,
            input.Coordinate);
        
        return MapPlaceDto(place);
    }
    
    public async Task DeletePlaceAsync(
        Guid id) 
        => await repository.DeleteAsync(id);

    private static PlaceDto MapPlaceDto(
        Place place)
        => new(
            place.Id,
            place.Title,
            place.Description,
            place.Images,
            new(place.Latitude, place.Longitude));
}