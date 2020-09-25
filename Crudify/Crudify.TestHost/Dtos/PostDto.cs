using FluentValidation;
using System;

namespace Crudify.TestHost.Dtos
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }

    public class PostDto : IIdentity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid BlogId { get; set; }
    }
}
