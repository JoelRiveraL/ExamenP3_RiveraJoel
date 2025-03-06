using System.ComponentModel.DataAnnotations;

namespace TDDTestingMVC.Data
{
    public class Cliente
    {
        [Required]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener exactamente 10 dígitos.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "La cédula solo puede contener números.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Los apellidos solo pueden contener letras y espacios.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Cliente), "ValidarFechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression("^[0-9]{7,10}$", ErrorMessage = "El teléfono debe tener entre 7 y 10 dígitos y solo números.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres.")]
        public string Direccion { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Método de validación personalizada para evitar fechas futuras
        public static ValidationResult ValidarFechaNacimiento(DateTime fecha, ValidationContext context)
        {
            if (fecha > DateTime.Today)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser futura.");
            }
            return ValidationResult.Success;
        }
    }
}
