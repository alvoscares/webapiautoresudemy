using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIAutores.Controllers.V1;
using WebAPIAutores.Tests.Mocks;

namespace WebAPIAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class RootControllerTests
    {
        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos4Links()
        {
            // Preparacion
            // El RootController teine dependencias por lo que vamos a crear un Mock (es una clase que va a suplantar la dependencia)
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Success();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            // Ejecucion
            var resultado = await rootController.Get();

            // Verificacion
            // Si el Get me devuelve 4 rutas esta todo ok
            Assert.AreEqual(4, resultado.Value.Count());

        }

        [TestMethod]
        public async Task SiUsuarioNoEsAdmin_Obtenemos2Links()
        {
            // Preparacion
            // El RootController teine dependencias por lo que vamos a crear un Mock (es una clase que va a suplantar la dependencia)
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Failed(); 
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            // Ejecucion
            var resultado = await rootController.Get();

            // Verificacion
            // Si el Get me devuelve 4 rutas esta todo ok
            Assert.AreEqual(2, resultado.Value.Count());

        }

        [TestMethod]
        public async Task SiUsuarioNoEsAdmin_Obtenemos2Links_UsandoMoq()
        {
            // Preparacion
            // En este caso vamos a usar la libreria Moq que me proporciona los mock sin necesidad que yo los cree como clases
            //var authorizationService = new AuthorizationServiceMock();
            //authorizationService.Resultado = AuthorizationResult.Failed();
            var mockAuthorizationService = new Mock<IAuthorizationService>();
            mockAuthorizationService.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<IEnumerable<IAuthorizationRequirement>>()
                )).Returns(Task.FromResult(AuthorizationResult.Failed()));

            mockAuthorizationService.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<string>()
                )).Returns(Task.FromResult(AuthorizationResult.Failed()));

            var mockURLHelper = new Mock<IUrlHelper>();
            mockURLHelper.Setup(x =>
            x.Link(It.IsAny<string>(),
            It.IsAny<object>()))
                .Returns(string.Empty);

            var rootController = new RootController(mockAuthorizationService.Object);
            //rootController.Url = new URLHelperMock();
            rootController.Url = mockURLHelper.Object; // El .Object es el que instancia una clase a partir de la interface dada

            // Ejecucion
            var resultado = await rootController.Get();

            // Verificacion
            // Si el Get me devuelve 4 rutas esta todo ok
            Assert.AreEqual(2, resultado.Value.Count());

        }

    }
}
