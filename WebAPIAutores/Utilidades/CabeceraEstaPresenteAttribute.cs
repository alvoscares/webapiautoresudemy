using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace WebAPIAutores.Utilidades
{
    // IActionConstraint nos permite tener una logica personalizada para indicar que endpoint quiero que se ejecute
    public class CabeceraEstaPresenteAttribute : Attribute, IActionConstraint
    {
        private readonly string cabecera;
        private readonly string valor;

        public CabeceraEstaPresenteAttribute(string cabecera, string valor)
        {
            this.cabecera = cabecera;
            this.valor = valor;
        }
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            // Logica que va a determinar que si la peticion Http tiene la cabezera y el valor indicado,
            // se debe utilizar el endpoint donde se encuentra ese atributo 
            var cabeceras = context.RouteContext.HttpContext.Request.Headers;

            if (!cabeceras.ContainsKey(cabecera))
            {
                return false;
            }

            return string.Equals(cabeceras[cabecera], valor, StringComparison.OrdinalIgnoreCase);
        }
    }
}
