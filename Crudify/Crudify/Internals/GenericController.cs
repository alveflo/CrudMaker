using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crudify.Internals
{
    internal class GenericController<TDto, TEntity, TDbContext> : ControllerBase
        where TDto : class, IIdentity
        where TEntity : class, IIdentity
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        public GenericController(TDbContext dbContext, IMapper mapper, IValidator<TDto> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DbContextAccessor.GetDbSet<TEntity, TDbContext>(_dbContext).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TDto dto)
        {
            var result = Validate(dto);
            if (result != null)
                return BadRequest(result);

            var mapped = _mapper.Map<TEntity>(dto);
            var dbSet = DbContextAccessor.GetDbSet<TEntity, TDbContext>(_dbContext);

            dbSet.Add(mapped);
            await _dbContext.SaveChangesAsync();
            return Ok();
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
