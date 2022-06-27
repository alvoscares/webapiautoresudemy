using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPIAutores.Utilidades
{
    public class AgregarParametroHATEOAS : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            // Filtro para que solo se aplique a los metodos GET
            if (context.ApiDescription.HttpMethod != "GET")
            {
                return;
            }

            // Si es nulo lo inicializo
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Agrego el parametro. Los parametro son las operaciones que puedo realizar con una ruta (ej: agregar una cabezera)
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "incluirHATEOAS",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}
