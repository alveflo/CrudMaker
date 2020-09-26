using System.Collections.Generic;

namespace CrudMaker.Internals
{
    internal class CustomValidationResult
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
