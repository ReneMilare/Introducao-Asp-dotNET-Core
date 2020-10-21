using Alura.ListaLeitura.App.Logica;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    /* A classe "Startup" precisa implementar o método "Configure" para startar o servidor */
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(); //Disponibiliza o serviço de roteamento padrão do Asp.NET Core
        }

        /* Configurando a sequêncai de resposta (Request Pipeline), e a configuração desse fluxo requisição-resposta, também fica em um tipo do Asp.NET Core que é o "IApplicationBuilder", que vai construir todo o pipeline para a aplicação.*/
        public void Configure(IApplicationBuilder app)
        {
            // Utilizando o roteamento nativo do Asp.NET Core
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosLogica.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLogica.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLogica.LivrosLidos);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.ExibeDetalhes); // Rota com templete e restrição de tipo
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivroParaLer); // Rota com templete
            builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);
            
            var rotas = builder.Build(); // Construindo as rotas usando o método "Build"
            app.UseRouter(rotas); //Determina que o estágio terminal do request pipeline irá tratar as rotas definidas pela coleção armazenada na variável "rotas". Sem ela não seria possível tratar qualquer requisição na aplicação.
        }
    }
}