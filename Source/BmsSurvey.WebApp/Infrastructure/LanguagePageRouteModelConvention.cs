namespace BmsSurvey.WebApp.Infrastructure
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    public class LanguagePageRouteModelConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            var selectorCount = model.Selectors.Count;
            for (var i = 0; i < selectorCount; i++)
            {
                var selector = model.Selectors[i];
                model.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = AttributeRouteModel.CombineTemplates("{culture=bg}", selector.AttributeRouteModel.Template),
                        Order = -1,
                    }
                });
            }
        }
    }
}
