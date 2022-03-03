using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AlkemyChallenge.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.OriginalModelName;
            var valueProvider = bindingContext.ValueProvider.GetValue(propertyName);

            if(valueProvider == ValueProviderResult.None)
                return Task.CompletedTask;

            try
            {
                var deserializerValue = JsonConvert.DeserializeObject<T>(valueProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializerValue);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(propertyName, @"El valor ingreado no es válido");
            }
                
            return Task.CompletedTask;
        }
    }
}
