using System.ComponentModel.DataAnnotations;

namespace WebAPIAutores.Validaciones
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
                
            var primeraLatra = value.ToString()[0].ToString();

            if(primeraLatra != primeraLatra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser en mayuscula");
            }

            return ValidationResult.Success;
        }
    }
}
