using Abp.Application.Services.Dto;
using System;

namespace Schola.Users.Dto
{
    public class AssistanceFullViewDtoFE : EntityDto<long>
    {
        public string fullName { get; set; }
        public DateTime? Date { get; set; }
        public long UserStudentId { get; set; }
        public int CourseId { get; set; }
        public bool isPresent { get; set; } = false;
    }
}
