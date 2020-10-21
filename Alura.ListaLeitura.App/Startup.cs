using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    /* A classe "Startup" precisa implementar o método "Configure" para startar o servidor */
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        /* Configurando a sequêncai de resposta (Request Pipeline), e a configuração desse fluxo requisição-resposta, também fica em um tipo do Asp.NET Core que é o "IApplicationBuilder", que vai construir todo o pipeline para a aplicação.*/
        public void Configure(IApplicationBuilder app)
        {
            // Utilizando o roteamento nativo do Asp.NET Core
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer); // Rota com templete
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes); // Rota com templete e restrição de tipo
            var rotas = builder.Build(); // Construindo as rotas usando o método "Build"

            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro() 
            { 
                // Recuperando os valores das rota com templete e convertendo em string
                Titulo = Convert.ToString(context.GetRouteValue("nome")),
                Autor = Convert.ToString(context.GetRouteValue("autor").ToString())
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso.");
        }

        public Task Roteamento(HttpContext context)
        {

            var _repo = new LivroRepositorioCSV();
            var caminhoAtentidos = new Dictionary<string, RequestDelegate>
            {
                {"/Livros/ParaLer", LivrosParaLer },
                {"/Livros/Lendo", LivrosLendo },
                {"/Livros/Lidos", LivrosLidos }
            };

            /* A propriedade "Path" da propriedade "Request", "Path" nos fornece todo o endereço tirando as partes de domínio, servidor, portas, etc. */
            if (caminhoAtentidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhoAtentidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho inexistente");
        }

        /* Toda informação que estiver encapsulada em uma requisição específica, fica armazenada em objetos do tipo "HttpContext", ou seja, o "HttpContext" armazena toda a informação de uma requisição. Precisamos retornar um objeto do tipo Task para que o "IApplicationBuilder" consiga construir o pipeline. Objetos do tipo Task são abordados no curso de paralelismo. */
        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            /* Quando a requisição chegar vamos responder escrevendo na tela */
            return context.Response.WriteAsync(_repo.ParaLer.ToString());

        }public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            
            return context.Response.WriteAsync(_repo.Lendo.ToString());

        }public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }
    }
}