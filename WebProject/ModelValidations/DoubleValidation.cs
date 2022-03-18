using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class DoubleValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;

            if (value == null)
            {
                errorType = "không được bỏ trống";

            }
            else if (!IsValidDouble(value.ToString()))
            {
                errorType = "không hợp lệ";

            }
            else if (!IsValidPositive(value.ToString()))
            {
                errorType = "phải lớn hơn 0 ";
            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);

        }

        bool IsValidPositive(string number)
        {
            try
            {
                var db = double.Parse(number);
                if (db > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        bool IsValidDouble(string number)
        {
            try
            {
                var db = double.Parse(number);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
