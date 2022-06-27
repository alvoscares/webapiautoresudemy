using System.ComponentModel.DataAnnotations;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }

    }
}
