using FluentValidation;
using System;

namespace CrudMaker.TestHost.Dtos
{
    public class BlogDtoValidator : AbstractValidator<BlogDto>
    {
        public BlogDtoValidator()
        {
            RuleFor(x => x.Url).NotEmpty();
        }
    }

    public class BlogDto : IIdentity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
    }
}
