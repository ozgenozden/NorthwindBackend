using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerts.Validation
{
    public static class ValidationTool
    {
        //IValidator aslında ProductValidator un base i olan bir interface dir.
        //object entity yaparak hem entity claslarını hem de dto claslarını geçmesini sağlamış oluruz.
        public static void Validate(IValidator validator,object entity)
        {
              
            var result=validator.Validate((IValidationContext)entity); 
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
