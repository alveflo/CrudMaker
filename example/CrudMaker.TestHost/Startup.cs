using AutoMapper;
using CrudMaker.TestHost.Database;
using CrudMaker.TestHost.Database.Repositories;
using CrudMaker.TestHost.Dtos;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrudMaker.TestHost
{
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

            services.AddCrud<TestDbContext>(options =>
            {
                options.Add<BlogDto, Blog, BlogRepository>("/blogs");
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
            app.UseRouting();

            app.UseAuthorization();
            app.UseCrudMakerOData();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
