using FindOutOurHouse.DAL.Coordinates;
using FindOutOurHouse.DAL.Places;
using Microsoft.EntityFrameworkCore;

namespace FindOutOurHouse.DAL.Repositories;

/// <inheritdoc cref="IPlaceRepository"/>
public class PlaceRepository(EfDbContext context) : IPlaceRepository
{
    /// <inheritdoc/>
    public async Task<Place?> GetAsync(
        Guid id)
        => await Task.Run(() => context.Places.AsNoTracking().FirstOrDefault(pl => pl.Id == id));

    /// <inheritdoc/>
    public async Task<IEnumerable<Place>> GetListAsync(
        string? title = null,
        string? description = null)
    {
        var query = await Task.Run(() => context.Places.AsNoTracking().AsEnumerable());
        
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
        double radius) 
        => await Task.Run(() => context.Places.AsNoTracking().Include(p => p.Coordinate)
                .Where(p 
                    => Math.Pow(p.Coordinate.Longitude - center.Longitude, 2) 
                    * Math.Pow(p.Coordinate.Latitude - center.Latitude, 2) <= Math.Pow(radius, 2)));

    /// <inheritdoc/>
    public async Task<Place> CreateAsync(
        string title,
        string? description,
        Coordinate coordinate)
    {
        Place place = new(
            title, 
            description,
            coordinate);
        
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
            place.Coordinate = coordinate;
        
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
}
