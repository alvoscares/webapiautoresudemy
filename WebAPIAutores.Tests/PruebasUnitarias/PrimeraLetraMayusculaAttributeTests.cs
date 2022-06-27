using System.ComponentModel.DataAnnotations;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            // Una prueba se divide en 3 partes
            // Preparacion
            // Aca tengo que instanciar cualquier olbjeto/clase que yo necesite para la fase de puebas 
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var valor = "alvaro";
            var valContext = new ValidationContext(new { Name = valor });

            // Ejecucion
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            // Verificacion
            Assert.AreEqual("La primera letra debe ser en mayuscula", resultado.ErrorMessage);
        }

        [TestMethod]
        public void ValorNulo_NoDevuelveError()
        {
            // Una prueba se divide en 3 partes
            // Preparacion
            // Aca tengo que instanciar cualquier olbjeto/clase que yo necesite para la fase de puebas 
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = null;
            var valContext = new ValidationContext(new { Name = valor });

            // Ejecucion
            // Cuando no hay ningun error el GetValidationResult retorna un null
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            // Verificacion
            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
        {
            // Una prueba se divide en 3 partes
            // Preparacion
            // Aca tengo que instanciar cualquier olbjeto/clase que yo necesite para la fase de puebas 
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = "Alvaro";
            var valContext = new ValidationContext(new { Name = valor });

            // Ejecucion
            // Cuando no hay ningun error el GetValidationResult retorna un null
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            // Verificacion
            Assert.IsNull(resultado);
        }
    }
}