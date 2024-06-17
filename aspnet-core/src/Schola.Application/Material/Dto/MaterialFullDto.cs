using Schola.Materiales;

namespace Schola.Material.Dto
{
    public class MaterialFullDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public MaterialType MaterialType { get; set; }
        public string? Url { get; set; }
        public int CourseId { get; set; }
    }
}
