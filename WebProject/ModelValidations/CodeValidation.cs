using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class CodeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;

            if (value == null)
            {
                errorType = "không được bỏ trống !";
            }
            else if (!IsValidCode(value.ToString()))
            {
                errorType = "không chứa các kí tự đặt biệt và dấu !";
            }
            else if (!IsValidStringLenght(value.ToString()))
            {
                errorType = "độ dài phải 5 kí tự !";
            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);
        }

        bool IsValidCode(string item)
        {
            var c = Regex.IsMatch(item, @"^[a-zA-Z0-9]+$");
            
            if (c)
            {
                return true;
            }

            return false;
        }

        bool IsValidStringLenght(string item)
        {
            if (item.Length == 5)
            {
                return true;
            }

            return false;
        }
    }
}
