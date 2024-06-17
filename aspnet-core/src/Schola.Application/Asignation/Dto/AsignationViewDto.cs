using Abp.Application.Services.Dto;
using System;

namespace Schola.Asignation.Dto
{
    public class AsignationViewDto : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CourseId { get; set; }
    }
}
