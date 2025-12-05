using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Exceptions;
public class BusinessException : CustomException
{
    public BusinessException(string code, string message) : base(code, message) { }
} 
