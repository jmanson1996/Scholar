using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Schola.Material.Dto;

namespace Schola.Material
{
    public class MaterialAppService : AsyncCrudAppService<Materiales.Material, MaterialViewDto, int, PagedResultRequestDto, MaterialFullDto, MaterialViewDto>, IMaterialAppService
    {
        private readonly IRepository<Materiales.Material, int> _courseRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public MaterialAppService(IRepository<Materiales.Material, int> repository)
            : base(repository)
        {
            _courseRepository = repository;
        }
    }
}
