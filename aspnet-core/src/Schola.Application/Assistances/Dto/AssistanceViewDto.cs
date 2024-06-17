using Abp.Application.Services.Dto;
using System;

namespace Schola.Assistances.Dto
{
    public class AssistanceViewDto : EntityDto
    {
        public DateTime Date { get; set; }
        public long UserStudentId { get; set; }
        public int CourseId { get; set; }
        public bool isPresent { get; set; } = false;
    }
}
