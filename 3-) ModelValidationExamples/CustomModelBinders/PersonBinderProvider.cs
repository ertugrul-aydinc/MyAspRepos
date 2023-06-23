using System;
using _3___ModelValidationExamples.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace _3___ModelValidationExamples.CustomModelBinders
{
	public class PersonBinderProvider : IModelBinderProvider
	{
		public PersonBinderProvider()
		{
		}

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(Person))
                return new BinderTypeModelBinder(typeof(PersonModelBinder));

            return null;
        }
    }
}

