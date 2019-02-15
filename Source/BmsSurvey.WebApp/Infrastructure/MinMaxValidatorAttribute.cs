namespace BmsSurvey.WebApp.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Interfaces;
    using Resources;

    [AttributeUsage(AttributeTargets.Property)]
    public class MinMaxValidator : ValidationAttribute
    {
        private readonly int minValue;
        private readonly int maxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinMaxValidator" /> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="resourceService">The resource service.</param>
        public MinMaxValidator(int minValue, string errorMessage, int maxValue)
        : base(errorMessage)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var intValue = (int)value ;

            if (intValue < this.minValue || intValue > this.maxValue)
            {
                var localizerService = (ILocalizationService<LayoutResource>) validationContext.GetService(typeof(ILocalizationService<LayoutResource>));
                var errString = localizerService?.GetLocalizedHtmlString(this.ErrorMessageString);
                var errMessage = String.Format(errString, validationContext.DisplayName, minValue, maxValue);
                return new ValidationResult(errMessage);
            }

            return ValidationResult.Success;
        }
    }
}
