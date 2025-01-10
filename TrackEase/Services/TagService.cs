using TrackEase.Abstraction;
using TrackEase.Models;
using TrackEase.Services.Interface;

namespace TrackEase.Services;

public class TagService : BaseService<Tag>, ITagService
{
    private List<Tag> _tags;
    private static readonly string AppTagsFilePath = Util.Util.GetAppTagsFilePath();

    public TagService()
    {
        _tags = GetAll(AppTagsFilePath);
    }

    public List<Tag> GetAllTags()
    {
        return _tags;
    }
    public bool AddTag(Tag tag)
    {
        try
        {
            // Normalize input to ensure consistent comparison
            string normalizedTagName = tag.TagName?.Trim().ToLowerInvariant();

            // Prevent duplicate tag names
            if (string.IsNullOrWhiteSpace(normalizedTagName))
            {
                Console.WriteLine("Tag name cannot be empty.");
                return false;
            }

            if (_tags.Any(t => t.TagName?.Trim().ToLowerInvariant() == normalizedTagName))
            {
                Console.WriteLine("Tag already exists.");
                return false;
            }

            // Assign a unique Id
            tag.Id = _tags.Any() ? _tags.Max(t => t.Id) + 1 : 1;

            // Add the new tag to the collection
            _tags.Add(tag);

            // Save all tags to persistent storage
            SaveAll(_tags, AppTagsFilePath);

            Console.WriteLine("Tag added successfully.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding tag: {ex.Message}");
            return false;
        }
    }

}