using Abp.Application.Services.Dto;
using System;

namespace Schola.Comments.Dto
{
    public class CommentViewDto : EntityDto
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public long UserCommentId { get; set; }
        public int CourseId { get; set; }
    }
}
