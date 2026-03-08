using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity,object key) : 
            base($"{entity} bulunamadı. Anahtar: {key}") { }

    }
}
