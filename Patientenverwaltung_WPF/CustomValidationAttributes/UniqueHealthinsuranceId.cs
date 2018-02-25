using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    public class UniqueHealthinsuranceId : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contains = HealthinsuranceViewModel.SharedViewModel().Healthinsurances.Select(x => x.HealthinsuranceId).Contains(int.Parse(value.ToString()));

            if (contains)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            else
                return ValidationResult.Success;
        }
    }
}
