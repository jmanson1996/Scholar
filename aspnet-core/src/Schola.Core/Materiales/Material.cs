using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Schola.Courses;

namespace Schola.Materiales
{
    public class Material : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public MaterialType MaterialType { get; set; }
        public string? Url { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
    public enum MaterialType
    {
        Video,
        Document,
        Link
    }
}
