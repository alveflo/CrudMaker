using System.Collections.Generic;

namespace Crudify.Internals
{
    internal class CustomValidationResult
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
