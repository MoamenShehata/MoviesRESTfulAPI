using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace MoviesRESTfulAPI.Model_Binders
{
    public class IEnumerableModelBinder<T> : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(IEnumerable<T>))
            {
                return false;
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            var converter = TypeDescriptor.GetConverter(typeof(T));

            var list = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => converter.ConvertFromString(s.Trim())).ToArray();

            var typedArray = Array.CreateInstance(typeof(T), list.Length);
            list.CopyTo(typedArray, 0);
            bindingContext.Model = typedArray;

            return true;
        }
    }
}