using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Bir veya birden fazla doğrulama hatası oluştu.")
        {  
            Errors = errors; 
        }

    }
}
