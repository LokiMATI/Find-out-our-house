using FindOutOurHouse.DAL.Coordinates;
using FindOutOurHouse.DAL.Places;
using Microsoft.EntityFrameworkCore;

namespace FindOutOurHouse.DAL.Repositories;

/// <inheritdoc cref="IPlaceRepository"/>
public class PlaceRepository(EfDbContext context) : IPlaceRepository
{
    /// <inheritdoc/>
    public async Task<Place?> GetAsync(
        Guid id,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None)
    {
        var query = await GetQueryAsync(includeTypes);
        return query.FirstOrDefault(x => x.Id == id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Place>> GetListAsync(
        string? title = null,
        string? description = null,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None)
    {
        var query = await GetQueryAsync(includeTypes);
        
        if (title is not null)
            query = query.Where(p
                => p.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase));
        if (description is not null)
            query = query.Where(p
                => p.Description != null
                   && p.Description.Contains(description, StringComparison.InvariantCultureIgnoreCase));

        return query;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Place>> GetListWithinRadiusAsync(
        Coordinate center,
        double radius,
        PlaceQueryIncludeTypes includeTypes = PlaceQueryIncludeTypes.None)
    {
        var query= await GetQueryAsync(includeTypes);
        
         query = await Task.Run(() => query.Where(p
                => Math.Pow(p.Longitude - center.Longitude, 2)
                * Math.Pow(p.Latitude - center.Latitude, 2) <= Math.Pow(radius, 2)));
        
        return query;
    }

    /// <inheritdoc/>
    public async Task<Place> CreateAsync(
        string title,
        string? description,
        Coordinate coordinate)
    {
        Place place = new(
            title, 
            description,
            coordinate.Latitude,
            coordinate.Longitude);
        
        await context.Places.AddAsync(place);
        await context.SaveChangesAsync();
        
        return place;
    }

    /// <inheritdoc/>
    public async Task<Place> UpdateAsync(
        Guid id,
        string? title,
        string? description,
        List<Guid>? images,
        Coordinate? coordinate)
    {
        var place = await context.Places.FirstOrDefaultAsync(p => p.Id == id);
        
        if (place is null)
            throw new KeyNotFoundException("Place not found");
        
        if (title is not null)
            place.Title = title;
        if (description is not null && description.Length > 0)
            place.Description = description;
        if (images is not null && images.Count > 0)
            place.Images = images;
        if (coordinate is not null)
        {
            place.Latitude = coordinate.Latitude;
            place.Longitude = coordinate.Longitude;
        }
        
        await Task.Run(() => context.Places.Update(place));
        await context.SaveChangesAsync();
        
        return place;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(
        Guid id)
    {
        var place = await context.Places.FirstOrDefaultAsync(p => p.Id == id);
        
        if (place is null)
            throw new KeyNotFoundException("Place not found");
        
        await Task.Run(() => context.Places.Remove(place));
        await context.SaveChangesAsync();
    }
    
    private async Task<IEnumerable<Place>> GetQueryAsync(PlaceQueryIncludeTypes includeTypes)
     {
         IEnumerable<Place> query;
         if (includeTypes.HasFlag(PlaceQueryIncludeTypes.Images))
             query = await Task.Run(() => context.Places.AsNoTracking().Include(p => p.Images).AsEnumerable());
         else 
             query = await Task.Run(() => context.Places.AsNoTracking().AsEnumerable());
         return query;
     }
}
