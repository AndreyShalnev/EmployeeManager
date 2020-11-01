using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace EmployeeManager.Validation
{
    public class RegexValidationRule : ValidationRule
    {
        public string RegexText { get; set; }
        public RegexOptions RegexOptions { get; set; }
        public object ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = ValidationResult.ValidResult;
            if (!string.IsNullOrEmpty(this.RegexText))
            {
                var text = value as string ?? String.Empty;
                if (!Regex.IsMatch(text, this.RegexText, this.RegexOptions))
                    result = new ValidationResult(false, this.ErrorMessage);
            }

            return result;
        }
    }
}
