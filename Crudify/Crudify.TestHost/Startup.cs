using AutoMapper;
using Crudify.TestHost.Database;
using Crudify.TestHost.Database.Repositories;
using Crudify.TestHost.Dtos;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crudify.TestHost
{
    public class TestDto { }

    public class TestEntity { }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TestDatabase"))
            );
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddControllers();

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BlogDto>());
            //services.AddOData();

            services.AddCrud<TestDbContext>(options =>
            {
                //options.Add<BlogDto, Blog, BlogRepository>("/blogs");
                options.Add<PostDto, Post>("/posts");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //var builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Post>("posts");
            //builder.Namespace = typeof(Post).Namespace;
            //var edmModel = builder.GetEdmModel();


            app.UseRouting();
            //app.UseOData(edmModel);

            app.UseAuthorization();
            app.UseAutoCrudOData();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc(routeBuilder =>
            //{
            //    // and this line to enable OData query option, for example $filter
            //    routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

            //    routeBuilder.MapODataServiceRoute("odata", "odata", edmModel);

            //    // uncomment the following line to Work-around for #1175 in beta1
            //    // routeBuilder.EnableDependencyInjection();
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.Select().Filter().OrderBy().Count().Expand().MaxTop(100);
            //    endpoints.MapODataRoute("odata", "odata", edmModel);
            //});
        }
    }
}
