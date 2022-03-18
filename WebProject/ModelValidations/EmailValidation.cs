using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class EmailValidation : ValidationAttribute
    {
        public EmailValidation(string sepcialType)
        {
            this.SepcialType = sepcialType;
        }
        public string SepcialType { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            //          string errorType;
            if (value != null)
            {
                errorType = "Không được bỏ trống";
            }
            else if (!string.IsNullOrEmpty(SepcialType) && !IsValidEmail(value.ToString()))
            {
                errorType = "không hợp lệ";
            }
            else
            {
                return ValidationResult.Success;
            }
            ErrorMessage = $"{validationContext.DisplayName} Dữ liệu  {errorType}.";
            return new ValidationResult(ErrorMessage);
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
