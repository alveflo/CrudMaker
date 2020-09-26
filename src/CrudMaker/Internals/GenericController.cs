using AutoMapper;
using AutoMapper.QueryableExtensions;
using CrudMaker.Abstractions;
using FluentValidation;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMaker.Internals
{
    internal class GenericController<TDto, TEntity, TDbContext> : ControllerBase
        where TDto : class, IIdentity
        where TEntity : class, IIdentity
        where TDbContext : DbContext
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        public GenericController(IRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<TDto> Get()
        {
            return _repository.GetQueryable()
                .ProjectTo<TDto>(_mapper.ConfigurationProvider);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<TEntity, TDto>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Post(Guid id, [FromBody] TDto dto)
        {
            var result = Validate(dto);
            if (result != null)
                return BadRequest(result);

            var existingEntity = await _repository.GetAsync(dto.Id);
            if (existingEntity == null)
                return NotFound();

            existingEntity = _mapper.Map(dto, existingEntity);

            await _repository.UpdateAsync(existingEntity);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TDto dto)
        {
            var result = Validate(dto);
            if (result != null)
                return BadRequest(result);

            var entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);
            var id = entity.Id;

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingEntity = await _repository.GetAsync(id);
            if (existingEntity != null)
            {
                await _repository.DeleteAsync(existingEntity);

                return Ok();
            }

            return NotFound();
        }

        private CustomValidationResult Validate(TDto dto)
        {
            if (_validator != null)
            {
                try
                {
                    _validator.ValidateAndThrow(dto);
                }
                catch (ValidationException ve)
                {
                    var failures = new Dictionary<string, string[]>();

                    var propertyNames = ve.Errors
                        .Select(e => e.PropertyName)
                        .Distinct();

                    foreach (var propertyName in propertyNames)
                    {
                        var propertyFailures = ve.Errors
                            .Where(e => e.PropertyName == propertyName)
                            .Select(e => e.ErrorMessage)
                            .ToArray();

                        failures.Add(propertyName, propertyFailures);
                    }

                    return new CustomValidationResult
                    {
                        Message = "One or more errors occurred.",
                        Errors = failures
                    };
                }
            }

            return null;
        }
    }
}
