<p align="center">
<h1 align="center">:rocket: CrudMaker</h1>
</p>

## Description
CrudMaker is intended to potentially save time and energy by automatically generating CRUD-endpoints for given entities. It has a tight coupling to OData, Entity Framework Core, AutoMapper and Fluentvalidation in order to achive rich queryablity, persistance, mapping entities to dto's and building business logic into operations by utilizing validators.

:warning: This repository is in an experimental stage

### Dependencies
This project is tightly coupled to
* [OData](https://www.odata.org/)
* [AutoMapper](https://automapper.org/)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [FluentValidation aspnet core](https://docs.fluentvalidation.net/en/latest/aspnet.html)

### How about...
> Can I use my own controllers alongside this library?

Yes, you can! If you'd like to use CrudMaker to generate crud operations for e.g. a `Blog` entity and also write your own controller for a `Post` entity with more complex business logic into it, that's totally fine.

> CrudMaker doesn't include relations when getting data

That's easily fixed by implementing a custom repository (shown below)

# Usage
When setting up a CRUD-operation (as below) with CrudMaker you'll get the following endpoints:
```csharp
services.AddCrud<TestDbContext>(options =>
{
    options.Add<BlogDto, Blog>("/blogs");
});
```
* `GET /api/blogs` Querying blogs by using OData, e.g.:
    * `GET /api/blogs?$select=Property1, Property2$top=10$skip=10` 

    (This will ofcourse return a projection to the provided dto rather than the database entity to not expose sensitive data)
* `GET /api/blogs/{id}` Gets `Blog` by id
* `POST /api/blogs` with provided dto as parameter
* `PUT /api/blogs/{id}` with provided dto as parameter
* `DELETE /api/blogs/{id}`

# Setup
For complete setup inspiration, please visit `example/CrudMaker.TestHost`.

## Configuration
### Startup service collection configuration
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // An EF Core database context is required
    services.AddDbContext<TestDbContext>(...);
    // AutoMapper is required
    services.AddAutoMapper(typeof(AutoMapperProfile));

    services.AddControllers();

    // Endpoint routing is not supported due to OData configurations
    services.AddMvc(option => option.EnableEndpointRouting = false)
        // Fluent validation (aspnet core style) is recommended
        .AddFluentValidation(...);

    services.AddCrud<TestDbContext>(options =>
    {
        // Adds CRUD endpoints for Post operations on /posts
        options.Add<PostDto, Post>("/posts");

        // Adds CRUD endpoints for Blog operations on /blogs
        // with custom repository.
        options.Add<BlogDto, Blog, BlogRepository>("/blogs");
    });
}
```

### Startup application builder configuration
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    // Add CrudMaker OData support
    app.UseCrudMakerOData();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

## Entities and dtos
Both entities and dtos needs to be implemented using the `CrudMaker.IIdentity` interface in order to identify entities
```csharp
public class Blog : IIdentity
{
    public Guid Id { get; set; }
    ...
}

public class BlogDto : IIdentity
{
    public Guid Id { get; set; }
    ...
}
```

## Custom repositories
Custom repositories is implemented by implementing the interface `CrudMaker.Abstractions.IRepository<TEntity>` and configured when configuring your service collection
```csharp
services.AddCrud<TestDbContext>(options =>
{
    options.Add<BlogDto, Blog, BlogRepository>("/blogs");
});
```

## Validation
Validation of incoming dto's for `POST` and `PUT` is achieved by implementing `FluentValidation` validators for the entities. This is not required but it's highly recommended.
```csharp
public class BlogDtoValidator : AbstractValidator<BlogDto>
{
    public BlogDtoValidator()
    {
        RuleFor(x => x.Property).NotEmpty();
    }
}
```

## Entity<->Dto mapping
Mapping is done using `AutoMapper` and thus need to be defined both ways in your auto mapper profile.
```csharp
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BlogDto, Blog>()
            .ReverseMap();
    }
}
```

### License
The MIT License
