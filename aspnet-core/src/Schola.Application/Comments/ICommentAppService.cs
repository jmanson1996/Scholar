using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Schola.Comments.Dto;

namespace Schola.Comments
{
    public interface ICommentAppService : IAsyncCrudAppService<CommentViewDto, int, PagedResultRequestDto, CommentFullDto, CommentViewDto>
    {
    }
}
