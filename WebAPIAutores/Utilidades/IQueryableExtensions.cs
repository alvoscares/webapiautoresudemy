using WebAPIAutores.DTOs;

namespace WebAPIAutores.Utilidades
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            // En el return se calculan la cantidad de registro que se va a saltear la paginacion asi puede mostrar el restro
            // Ej: si estoy en la pagina 2 =>  2 - 1 = 1, 1 * RecirsPorPagina (supongamos que mostramos 10 por pagina) = 10
            // En la pagina 2 nos vamos a saltear los primeros 10 registros para mostrar desde el registro 10 hasta el 19 (diez registros)
            return queryable
                .Skip((paginacionDTO.Pagina - 1) * paginacionDTO.RecordsPorPagina)
                .Take(paginacionDTO.RecordsPorPagina);
        }
    }
}
