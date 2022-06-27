using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIAutores.Tests.Mocks
{
    // En este mock yo tengo que usar el metodo Link, y no me importa lo que devuelva ya que en la clase que se esta usando este mock el 
    // retorno del metodo link se utiliza como parametro del metodo, por lo tanto voy a hacer que retorne un string vacio
    public class URLHelperMock : IUrlHelper
    {
        public ActionContext ActionContext => throw new NotImplementedException();

        public string? Action(UrlActionContext actionContext)
        {
            throw new NotImplementedException();
        }

        [return: NotNullIfNotNull("contentPath")]
        public string? Content(string? contentPath)
        {
            throw new NotImplementedException();
        }

        public bool IsLocalUrl([NotNullWhen(true)] string? url)
        {
            throw new NotImplementedException();
        }

        public string? Link(string? routeName, object? values)
        {
            return "";
        }

        public string? RouteUrl(UrlRouteContext routeContext)
        {
            throw new NotImplementedException();
        }
    }
}
