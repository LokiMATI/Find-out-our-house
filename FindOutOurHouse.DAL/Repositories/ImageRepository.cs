using FindOutOurHouse.DAL.Images;
using Microsoft.EntityFrameworkCore;

namespace FindOutOurHouse.DAL.Repositories;

/// <inheritdoc cref="IImageRepository"/>
public class ImageRepository(EfDbContext context) : IImageRepository
{
    /// <inheritdoc/>
    public async Task<Image?> GetAsync(
        Guid id)
        => await context.Images.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

    /// <inheritdoc/>
    public async Task<IEnumerable<Image>> GetImagesAsync(List<Guid> ids)
        => await Task.Run(() => context.Images.AsNoTracking().Where(i => ids.Contains(i.Id)));

    /// <inheritdoc/>
    public async Task<Image> AddAsync(byte[] data)
    {
        Image image = new(data);
        await context.Images.AddAsync(image);
        await context.SaveChangesAsync();
        
        return image;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Image>> AddListAsync(List<byte[]> files)
    {
        List<Image> images = new();
        foreach (var file in files)
        {
            images.Add(await AddAsync(file));
        }
        
        return images;
    }

    /// <inheritdoc/>
    public async Task DeleteImageAsync(Guid id)
    {
        var image = await GetAsync(id);
        
        if (image is null)
            throw new KeyNotFoundException("Image not found");
        await Task.Run(() => context.Images.Remove(image));
        await context.SaveChangesAsync();
    }
}