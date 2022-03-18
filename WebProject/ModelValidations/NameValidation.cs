using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class NameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;

            if (value == null)
            {
                errorType = "không được bỏ trống";
            }
            
            else if (!IsValidStringLenght(value.ToString()))
            {
                errorType = "độ dài phải nhỏ hơn 30 kí tự";
            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);
        }

        bool IsValidStringLenght(string item)
        {
            if (item.Length < 30)
            {
                return true;
            }

            return false;
        }
    }
}
