using TrackEase.Abstraction;
using TrackEase.Models;
using TrackEase.Services.Interface;

namespace TrackEase.Services
{
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
                string normalizedTagName = tag.TagName?.Trim().ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(normalizedTagName))
                {
                    return false;
                }

                if (_tags.Any(t => t.TagName?.Trim().ToLowerInvariant() == normalizedTagName))
                {
                    return false;
                }

                tag.Id = _tags.Any() ? _tags.Max(t => t.Id) + 1 : 1;

                _tags.Add(tag);

                SaveAll(_tags, AppTagsFilePath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}