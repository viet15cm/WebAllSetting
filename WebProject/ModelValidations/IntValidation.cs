using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class IntValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;

            if (value == null)
            {
                errorType = "không được bỏ trống";
                Console.WriteLine(value.ToString());
            }
            else if (!IsValidZero((int)value))
            {
                errorType = "Số phải lớn hơn 0";

            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);

        }

        //bool IsValidInt(int number)
        //{
        //    try
        //    {
        //        var db = int.Parse(number);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        bool IsValidZero(int number)
        {
            if (number <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
