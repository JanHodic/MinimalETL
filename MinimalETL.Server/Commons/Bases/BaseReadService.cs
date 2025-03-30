using System;
using AutoMapper;
using MinimalETL.Server.Contracts.Bases;
using MinimalETL.Server.Models.Bases;

namespace MinimalETL.Server.Commons.Bases
{
    public class BaseReadService<Repository, T, Dto> : IBaseReadService<Dto>
            where Dto : class
            where T : BaseDateEntity
            where Repository : IBaseReadRepository<T>
    {
        protected ILogger<T> _eventLogger;
        protected Repository _repository;
        protected IMapper _mapper;

        public BaseReadService(ILogger<T> eventLogger, Repository repository, IMapper mapper)
        {
            _eventLogger = eventLogger;
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ICollection<Dto>> FindAllByIds(IEnumerable<Guid> ids)
        {
            var entities = await _repository.FindAllByIds(ids);
            var dtos = _mapper.Map<ICollection<Dto>>(entities);
            return dtos;
        }

        public virtual async Task<ICollection<Dto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<ICollection<Dto>>(entities);
            return dtos;
        }

        public virtual async Task<ICollection<Dto>> GetAllAsync(int from, int to)
        {
            var entities = await _repository.GetAllAsync(from, to);
            var dtos = _mapper.Map<ICollection<Dto>>(entities);
            return dtos;
        }

        public virtual async Task<Dto> GetAsync(Guid? id)
        {
            var entity = await _repository.GetAsync(id);
            var dto = _mapper.Map<Dto>(entity);
            return dto;
        }
    }
}

