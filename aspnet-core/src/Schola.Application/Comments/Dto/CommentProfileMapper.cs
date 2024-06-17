using AutoMapper;

namespace Schola.Comments.Dto
{
    public class CommentProfileMapper : Profile
    {
        public CommentProfileMapper()
        {
            CreateMap<Comment, CommentViewDto>().ReverseMap();
            CreateMap<Comment, CommentFullDto>().ReverseMap();
        }
    }
}
