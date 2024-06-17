using Abp.Application.Services.Dto;

namespace Schola.Assistances.Dto
{
    public class PagedAssistenceResultRequestDto : PagedResultRequestDto
    {
        public int CourseId { get; set; }
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
