namespace BmsSurvey.WebApp.Infrastructure

{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DateTimeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.HttpContext.Request.Query.ContainsKey(bindingContext.ModelName))
            {
                if (DateTime.TryParse(bindingContext.HttpContext.Request.Query[bindingContext.ModelName], out var result))
                {
                    bindingContext.Model = result;
                    bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
                }
            }


            return Task.CompletedTask;
        }
    }
}