using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPIAutores.Utilidades
{
    public class AgregarParametroXVersion: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {   
            // Si es nulo lo inicializo
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Agrego el parametro. Los parametro son las operaciones que puedo realizar con una ruta (ej: agregar una cabezera)
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-version",
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}
