using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Exceptions
{
   public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException(Type entityType, object id)
        : base("ENTITY_NOT_FOUND", $"{entityType.Name} with id '{id}' not found") { }
    }
}
