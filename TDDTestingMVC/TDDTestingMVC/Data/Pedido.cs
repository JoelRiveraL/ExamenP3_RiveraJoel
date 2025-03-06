using System;
using System.ComponentModel.DataAnnotations;

namespace TDDTestingMVC.Data
{
    public class Pedido
    {
        [Required]
        public int PedidoID { get; set; }

        [Required(ErrorMessage = "El ClienteID es obligatorio.")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Pedido), nameof(ValidarFechaPedido))]
        public DateTime FechaPedido { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El estado del pedido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede superar los 50 caracteres.")]
        public string Estado { get; set; } = "Pendiente";
        public static ValidationResult ValidarFechaPedido(DateTime fecha, ValidationContext context)
        {
            if (fecha > DateTime.Today)
            {
                return new ValidationResult("La fecha del pedido no puede ser en el futuro.");
            }
            return ValidationResult.Success;
        }
    }
}
