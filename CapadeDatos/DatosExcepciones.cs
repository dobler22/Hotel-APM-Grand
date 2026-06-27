using System;

namespace CapaDatos
{
    public class DatosExcepciones : Exception
    {
        public DatosExcepciones(string mensaje, Exception inner)
            : base(mensaje, inner) { }
    }
}
