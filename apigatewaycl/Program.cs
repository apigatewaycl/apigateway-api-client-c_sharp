using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apigatewaycl.api_client.sii;

namespace apigatewaycl
{
    class Program
    {
        static void Main(string[] args)
        {
            // VACIAR EN UN FUTURO


            /*
            Env prueba = new Env();
            // string version = "1.1";

            prueba.SetVariablesDeEntorno();

            Console.WriteLine("Bienvenido a la Prueba de API Gateway hecho en C#.");


            string opcion = "-1";

            while (opcion != "0")
            {
                Console.WriteLine("Seleccione una opción de prueba\n1 - Probar ActividadesEconomicas\n2 - Probar BheEmitidos Documentos\n3 - Probar Contribuyentes Ver Situación\n4 - Probar IndicadorAnualUF\n5 - Probar IndicadorMensualUF\n6 - Probar IndicadorDiarioUF\n0 - Salir\n");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Ha elegido Probar ActividadesEconomicas\n");

                        Console.WriteLine("Inserte categoría (1 o 2): ");
                        int categoria = Convert.ToInt32(Console.ReadLine());

                        ActividadesEconomicas actividades = new ActividadesEconomicas();

                        Dictionary<string, object> opcion1 = actividades.ListadoActividades(categoria);

                        foreach(var elemento in opcion1)
                        {
                            Console.WriteLine(elemento.ToString());
                        }
                        break;
                    case "2":
                        Console.WriteLine("Ha elegido Probar BheEmitidos Documentos\n");
                        string rut = Environment.GetEnvironmentVariable("TEST_CONTRIBUYENTE_RUT");
                        string clave = Environment.GetEnvironmentVariable("TEST_CONTRIBUYENTE_CLAVE");
                        
                        //Console.WriteLine("Inserte rut:");
                        //string rut = Console.ReadLine();
                        //Console.WriteLine("Inserta clave SII: ");
                        //string clave = Console.ReadLine();
                        
                        Dictionary<string, string> usuario = new Dictionary<string, string>()
                        {
                            { "usuario_rut", rut }, // rut SII sin puntos y con dígito verificador
                            { "usuario_clave", clave } // clave SII
                        };

                        Console.WriteLine("Inserte rut de emisor (ej: 14141414-X)");
                        string rutEmisor = Console.ReadLine();
                        Console.WriteLine("Inserte periodo (formato AAAAMM)");
                        string periodoEmision = Console.ReadLine();

                        BheEmitidas bhe = new BheEmitidas(kwargs: usuario);

                        List<Dictionary<string, object>> opcion2 = bhe.Documentos(rutEmisor, periodoEmision);

                        foreach (var elemento in opcion2)
                        {
                            Console.WriteLine(elemento.ToString());
                        }
                        break;
                    case "3":
                        Console.WriteLine("Ha elegido Probar Contribuyentes SituacionTributaria\n");

                        Console.WriteLine("Inserte rut de contribuyente (ej: 14141414-X)");
                        string rutContribuyente = Console.ReadLine();

                        Contribuyentes contribuyentes = new Contribuyentes();

                        Dictionary<string, object> opcion3 = contribuyentes.Situacion_tributaria(rutContribuyente);

                        foreach (var elemento in opcion3)
                        {
                            Console.WriteLine(elemento.ToString());
                        }
                        break;
                    case "4":
                        Console.WriteLine("Ha elegido Probar IndicadorAnualUf\n");

                        Console.WriteLine("Inserte año a consultar:");
                        int anio = Convert.ToInt32(Console.ReadLine());
                        Uf indicadorAnual = new Uf();

                        Dictionary<string, object> opcion4 = indicadorAnual.Anual(anio);

                        foreach (var elemento in opcion4)
                        {
                            Console.WriteLine(elemento.ToString());
                        }
                        break;
                    case "5":
                        Console.WriteLine("Ha elegido Probar IndicadorMensualUf\n");

                        Console.WriteLine("Inserte Periodo a consultar(formato AAAAMM):");
                        string periodo = Console.ReadLine();
                        Uf indicadorMensual = new Uf();

                        Dictionary<string, object> opcion5 = indicadorMensual.Mensual(periodo);

                        foreach (var elemento in opcion5)
                        {
                            Console.WriteLine(elemento.ToString());
                        }
                        break;
                    case "6":
                        Console.WriteLine("Ha elegido Probar IndicadorDiarioUf\n");

                        Console.WriteLine("Inserte Fecha a consultar(formato AAAA-MM-DD):");
                        string dia = Console.ReadLine();
                        Uf indicadorDiario = new Uf();

                        Console.WriteLine(indicadorDiario.Diario(dia).ToString());
                        break;
                    case "0":
                        Console.WriteLine("Gracias por probar API Gateway (Presione cualquier tecla para salir...)");
                        break;
                    default:
                        Console.WriteLine("Selecciona una opción válida...\n");
                        break;
                }
            }
            Console.ReadKey();
            */
        }
    }
}
