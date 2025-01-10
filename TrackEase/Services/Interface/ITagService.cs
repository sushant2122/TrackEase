using TrackEase.Models;

namespace TrackEase.Services.Interface;

public interface ITagService
{
    List<Tag> GetAllTags();

    bool AddTag(Tag tag);
}