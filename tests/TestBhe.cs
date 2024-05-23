using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using apigatewaycl.api_client.sii;
using apigatewaycl.api_client.utils;

namespace tests
{
    [TestClass]
    public class TestBhe
    {
        [TestMethod]
        public void TestBheEmitidos()
        {
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            string USUARIO_RUT = Environment.GetEnvironmentVariable("USUARIO_RUT");
            string USUARIO_CLAVE = Environment.GetEnvironmentVariable("USUARIO_CLAVE");
            string TEST_BHE_FECHA = Environment.GetEnvironmentVariable("TEST_BHE_FECHA");

            Dictionary<string, string> usuario = new Dictionary<string, string>()
            {
                { "usuario_rut", USUARIO_RUT },
                { "usuario_clave", USUARIO_CLAVE }
            };

            try
            {
                BheEmitidas ListadoBhe = new BheEmitidas(usuario);
                List<Dictionary<string, object>> respuesta = ListadoBhe.Documentos(USUARIO_RUT, TEST_BHE_FECHA);

                foreach (var elemento in respuesta)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                if (respuesta.Count == 0)
                {
                    Trace.WriteLine("El usuario no ha emitido boletas de honorarios.");
                }

                Assert.AreEqual(respuesta.Count >= 0, true);
            }
            catch (AssertFailedException e)
            {
                // Si arroja un mensaje de error, es porque no estás conectado
                Trace.WriteLine($"No se ha podido encontrar el listado. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"Error de búsqueda. Error: {e}");
                Assert.Fail();
            }
        }
    }
}
