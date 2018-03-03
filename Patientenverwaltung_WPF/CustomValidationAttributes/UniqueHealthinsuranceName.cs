using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    public class UniqueHealthinsuranceName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contains =
                HealthinsuranceViewModel.SharedViewModel().Healthinsurances.ToList()
                    .Exists(x => x.Name.Equals(value.ToString())) &&
                HealthinsuranceViewModel.SharedViewModel().NewHealthinsurance.Name != value.ToString();

            if (contains)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            else
                return ValidationResult.Success;
        }
    }
}
