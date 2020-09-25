using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SQLiteApp
{
    public class ValidationRuleRequiered : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            try
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    result = new ValidationResult(false, "field is requiered");
                }
            }
            catch(System.NullReferenceException)
            {
                result = new ValidationResult(false, "field is requiered");
            }
            
            return result;
        }
    }
}
