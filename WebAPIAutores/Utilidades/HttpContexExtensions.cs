using Microsoft.EntityFrameworkCore;

namespace WebAPIAutores.Utilidades
{
    // La clase es static porque quiero colocar un metodo de extencion 
    public static class HttpContexExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabezera<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double cantidad = await queryable.CountAsync();
            httpContext.Response.Headers.Add("cantidadTotalRegistros", cantidad.ToString());
        }
    }
}
