using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ArchivosException : Exception
    {
        public ArchivosException() : this("No se pudo guardar el archivo.\n") { }
        public ArchivosException(string message) : base(message) { }
        public ArchivosException(string message, Exception InnerException) : base(message) { }
    }
}
