using Microsoft.AspNetCore.Identity;

namespace WebAPIAutores.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public int LibroId { get; set; }
        // propiedad de navegacion. Me permiten hacer join de manera facil. En este caso un comentario esta relacionado con un Libro
        public Libro Libro { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
