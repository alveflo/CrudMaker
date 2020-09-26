using System;
using System.Linq;
using Crudify.TestHost.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Crudify.TestHost.Controllers
{
    //public class TestODataController : ODataController
    //{
    //    private readonly TestDbContext _testDbContext;

    //    public TestODataController(TestDbContext testDbContext)
    //    {
    //        _testDbContext = testDbContext;
    //    }

    //    [HttpGet]
    //    [EnableQuery]
    //    [ODataRoute("posts")]
    //    public DbSet<Post> Get()
    //    {
    //        return _testDbContext.Posts;
    //    }
    //}

    //[ApiController]
    //[Route("routes")]
    //public class RoutesController : ControllerBase
    //{
    //    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    //    public RoutesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    //    {
    //        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    //    }

    //    [HttpGet]
    //    public IActionResult Get()
    //    {
    //        var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items
    //            .SelectMany(x => x.EndpointMetadata)
    //           .ToList();
                
    //            //.Select(ad => new
    //            //{
    //            //    Name = ad.AttributeRouteInfo.Template,
    //            //    Method = ad.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First(),
    //            //}).ToList();

    //        var res = new 
    //        {
    //            Routes = routes
    //        };

    //        return Ok(res);
    //    }
    //}
}
