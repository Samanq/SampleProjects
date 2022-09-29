namespace DelegateSample;

public class PhotoProcessor
{
    public delegate void PhotoFilterHandler(Photo photo);
    public void Process(string path, PhotoFilterHandler filterHandler)
    {
        var photo = Photo.LoadPhoto(path);

        //var filters = new PhotoFilters();
        //filters.ApplyBrightness(photo);
        //filters.ApplyContrast(photo);
        //filters.Resize(photo);

        filterHandler(photo);


        photo.Save();
    }
}
