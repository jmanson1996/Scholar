using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Schola.Comments.Dto;
using System;
using System.Threading.Tasks;

namespace Schola.Comments
{
    public class CommentAppService : AsyncCrudAppService<Comment, CommentViewDto, int, PagedResultRequestDto, CommentFullDto, CommentViewDto>, ICommentAppService
    {
        private readonly IRepository<Comment, int> _courseRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public CommentAppService(IRepository<Comment, int> repository)
            : base(repository)
        {
            _courseRepository = repository;
        }

        public override async Task<CommentViewDto> CreateAsync(CommentFullDto input)
        {
            try
            {
                CheckCreatePermission();

                var entity = MapToEntity(input);

                await Repository.InsertAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }
    }
}
