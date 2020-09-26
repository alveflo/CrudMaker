using System;

namespace CrudMaker.Internals
{
    internal class CrudModel
    {
        public string Path { get; set; }
        public Type DtoType { get; set; }
        public Type EntityType { get; set; }
        public Type Repository { get; set; }
    }
}
