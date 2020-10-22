using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Mvc
{
    public class RoteamentoPadrao
    {
        public static Task TratamentoPadrao(HttpContext context)
        {
            //rota padrão: /<Classe>Logica/Metodo
            //{classe}/{metodo}

            var classe = context.GetRouteValue("classe").ToString();
            var nomeMetodo = context.GetRouteValue("metodo").ToString();

            var nomeCompleto = $"Alura.ListaLeitura.App.Logica.{classe}Logica";
            
            var tipo = Type.GetType(classe);
            var metodo = tipo.GetMethods().Where(m => m.Name == nomeMetodo).First();
            var requestDelegate = (RequestDelegate)Delegate.CreateDelegate(typeof(RequestDelegate), metodo);

            return requestDelegate.Invoke(context);
        }
    }
}
