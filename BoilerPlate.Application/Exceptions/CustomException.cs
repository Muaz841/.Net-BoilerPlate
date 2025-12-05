using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Exceptions;

    public abstract  class CustomException : Exception
    {
        public string Code { get; }
        public CustomException(string code, string message) : base(message) { Code = code; }
    }


