using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    /* A classe "Startup" precisa implementar o método "Configure" para startar o servidor */
    public class Startup
    {
        /* Configurando a sequêncai de resposta (Request Pipeline), e a configuração desse fluxo requisição-resposta, também fica em um tipo do Asp.NET Core que é o "IApplicationBuilder", que vai construir todo o pipeline para a aplicação.*/
        public void Configure(IApplicationBuilder app)
        {
            app.Run(LivrosParaLer);
        }

        /* Toda informação que estiver encapsulada em uma requisição específica, fica armazenada em objetos do tipo "HttpContext". Precisamos retornar um objeto do tipo Task para que o "IApplicationBuilder" consiga construir o pipeline. Objetos do tipo Task são abordados no curso de paralelismo. */
        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            /* Quando a requisição chegar vamos responder escrevendo na tela */
            return context.Response.WriteAsync(_repo.ParaLer.ToString());
        }
    }
}