using FindOutOurHouse.API.DTO.Places;
using FindOutOurHouse.DAL.Images;
using FindOutOurHouse.DAL.Places;
using FindOutOurHouse.DAL.Repositories;

namespace FindOutOurHouse.API.AppServices;

/// <summary>
/// Сервисы для работы с местами.
/// </summary>
public class PlaceService(IPlaceRepository placeRepository, IImageRepository imageRepository)
{
    public async Task<PlaceDto> GetPlaceAsync(
        Guid id,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None)
    {
        var place = await placeRepository.GetAsync(
            id,
            includeTypes);
        
        if (place is null)
            throw new KeyNotFoundException("Place not found.");
        
        return await MapPlaceDto(place);
    }

    public async Task<IEnumerable<PlaceDto>> GetPlacesAsync(
        string? title = null,
        string? description = null,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None)
    {
        var query = await placeRepository.GetListAsync(
            title,
            description,
            includeTypes);
        
        List<PlaceDto> dtos = new();
        foreach (var place in query)
            dtos.Add(await MapPlaceDto(place));

        return dtos;
    }

    public async Task<IEnumerable<PlaceDto>> GetPlacesAsync(
        PlaceGetDto input,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None)
    {
        var places = await placeRepository.GetListWithinRadiusAsync(
            input.Coordinate,
            input.Radius,
            includeTypes);

        List<PlaceDto> dtos = new();
        foreach (var place in places)
            dtos.Add(await MapPlaceDto(place));

        return dtos;
    }

    public async Task<PlaceDto> CreatePlaceAsync(
        PlaceCreateDto input)
    {
        var place = await placeRepository.CreateAsync(
            input.Title,
            input.Description,
            input.Coordinate);
        
        return await  MapPlaceDto(place);
    }

    public async Task<PlaceDto> UpdatePlaceAsync(
        Guid id,
        PlaceUpdateDto input)
    {
        List<Image> images = new();
        if (input.Images is not null && input.Images.Any())
            images.AddRange(await imageRepository.AddListAsync(input.Images));
        
        var place = await placeRepository.UpdateAsync(
            id,
            input.Title,
            input.Description,
            input.Images is not null && input.Images.Any() ?  images.Select(i => i.Id).ToList() : null,
            input.Coordinate);
        
        return await MapPlaceDto(place);
    }
    
    public async Task DeletePlaceAsync(
        Guid id)
    {
        var place = await placeRepository.GetAsync(id);
        await placeRepository.DeleteAsync(id);
        
        var images = await imageRepository.GetImagesAsync(place.Images);
        foreach (var image in images)
            await imageRepository.DeleteImageAsync(image.Id);
    }

    private async Task<PlaceDto> MapPlaceDto(
        Place place)
    {
        var images = await imageRepository.GetImagesAsync(place.Images);
        
        return new PlaceDto(
            place.Id,
            place.Title,
            place.Description,
            images,
            new(place.Latitude, place.Longitude));
    }
}