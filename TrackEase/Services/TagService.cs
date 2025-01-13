using TrackEase.Abstraction;
using TrackEase.Models;
using TrackEase.Services.Interface;

namespace TrackEase.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        private List<Tag> _tags;
        private static readonly string AppTagsFilePath = Util.Util.GetAppTagsFilePath();

        // Define default tags with their types
        private static readonly List<(string TagName, string Type)> DefaultTags = new List<(string TagName, string Type)>
        {
            ("Food", "Expense"),
            ("Drinks", "Expense"),
            ("Clothes", "Expense"),
            ("Gadgets", "Expense"),
            ("Miscellaneous", "Expense"),
            ("Fuel", "Expense"),
            ("Rent", "Expense"),
            ("EMI", "Debt"),
            ("Party", "Expense")
        };

        public TagService()
        {
            _tags = GetAll(AppTagsFilePath);
            SeedTags(); // Ensure tags are seeded on service initialization
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

        private void SeedTags()
        {
            foreach (var (tagName, type) in DefaultTags)
            {
                if (!_tags.Any(t => t.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase)))
                {
                    _tags.Add(new Tag
                    {
                        Id = _tags.Any() ? _tags.Max(t => t.Id) + 1 : 1,
                        TagName = tagName,
                        TagType = type // Set the type for the tag
                    });
                }
            }

            SaveAll(_tags, AppTagsFilePath); // Save the updated list to the file
        }
    }
}
