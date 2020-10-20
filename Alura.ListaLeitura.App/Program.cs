using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new LivroRepositorioCSV();

            /* A classe "WebHostBuilder" é responsável por construir um hospedeiro web, o método "Build" cria uma implementação da interface "IWebHost", precisamos também dizer qual é a implementação do modelo HTTP que estamos utilizando, fazemos isso com o método "UseKestrel", é preciso também uma classe que inicializa esse host utilizamos o método "UseStartup" passando a classe por ele. */
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();

            //ImprimeLista(_repo.ParaLer);
            //ImprimeLista(_repo.Lendo);
            //ImprimeLista(_repo.Lidos);
        }

        static void ImprimeLista(ListaDeLeitura lista)
        {
            Console.WriteLine(lista);
        }
    }
}
