using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    public class UniqueUsername : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contains =
                UserViewModel.SharedViewModel().Users.ToList()
                    .Exists(x => x.Username.Equals(value.ToString().Trim()));

            if (contains)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            else
                return ValidationResult.Success;
        }
    }
}
