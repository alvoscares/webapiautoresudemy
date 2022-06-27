using System.ComponentModel.DataAnnotations;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido xd")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres.")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }
    }
}
