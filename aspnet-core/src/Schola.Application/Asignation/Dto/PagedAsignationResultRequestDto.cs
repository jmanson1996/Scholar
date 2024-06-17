using Abp.Application.Services.Dto;

namespace Schola.Asignation.Dto
{
    public class PagedAsignationResultRequestDto : PagedResultRequestDto
    {
        public int CourseId { get; set; }
        public string Keyword { get; set; }
    }
}
