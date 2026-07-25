using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapadeLogica
{
    public class LogicaExcepciones : ApplicationException
    {
        public LogicaExcepciones(string message) : base(message)
        {
        }
        public LogicaExcepciones(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
