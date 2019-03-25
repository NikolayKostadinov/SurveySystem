//  ------------------------------------------------------------------------------------------------
//   <copyright file="FormInput.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.TagHelpers
{
    #region Using

    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    #endregion

    [HtmlTargetElement(Attributes = "form-input")]
    public class FormInputTagHelper : TagHelper
    {
        private readonly HtmlEncoder htmlEncoder;
        private readonly IHtmlGenerator htmlGenerator;

        public FormInputTagHelper(IHtmlGenerator htmlGenerator, HtmlEncoder htmlEncoder)
        {
            this.htmlGenerator = htmlGenerator;
            this.htmlEncoder = htmlEncoder;
        }

        [HtmlAttributeName("asp-for")] public ModelExpression For { get; set; }

        [HtmlAttributeNotBound] [ViewContext] public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.PreElement.AppendHtml("<div class=\"form-group row\">");
            output.PreElement.AppendHtml(CreateLabel()); //inWriter.ToString());
            output.PreElement.AppendHtml("<div class=\"col-sm-9\">");
            output.PostElement.AppendHtml(CreateValidator());
            output.PostElement.AppendHtml("</div></div>");
        }


        private TagBuilder CreateLabel() //TextWriter writer)
        {
            var tagBuilder = htmlGenerator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                null,
                new {@class = "col-sm-3 col-form-label" });
            return tagBuilder;
            //tagBuilder.WriteTo(writer, htmlEncoder);
        }


        private TagBuilder CreateValidator()
        {
            var tagBuilder = htmlGenerator.GenerateValidationMessage(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                null,
                null,
                new {@class = "text-danger"});
            return tagBuilder;
        }
    }
}