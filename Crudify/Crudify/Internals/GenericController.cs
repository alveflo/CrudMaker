using Microsoft.AspNetCore.Mvc;

namespace Crudify.Internals
{
    internal class GenericController<TDto, TEntity> : ControllerBase
        where TDto : class
        where TEntity : class
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] TDto value)
        {
            return Ok();
        }
    }
}
