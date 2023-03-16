using Entities;

namespace Infrastructure;

public class PhotoRepository
{
    private PhotoDbContext _photoDbContext;

    public PhotoRepository(PhotoDbContext context)
    {
        _photoDbContext = context;
    }

    public Photo SavePhoto(Photo photo)
    {
        _photoDbContext.PhotoTable.Add(photo);
        _photoDbContext.SaveChanges();
        return photo;
    }

    public List<Photo> GetAllPhotos()
    {
        return _photoDbContext.PhotoTable.ToList();
    }

    public void CreateDb()
    {
        _photoDbContext.Database.EnsureDeleted();
        _photoDbContext.Database.EnsureCreated();
        
    }
}