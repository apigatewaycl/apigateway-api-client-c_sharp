using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using apigatewaycl.api_client.sii;
using apigatewaycl.api_client.utils;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace tests
{
    [TestClass]
    public class TestIndicadores
    {

        [TestMethod]
        public void TestIndicadorAnual()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            string anio = Environment.GetEnvironmentVariable("TEST_UF_ANIO");
            Uf Indicador = new Uf();

            // Despliega el valor de la UF este año correctamente

            try
            {
                Dictionary<string, object> uf = Indicador.Anual(Convert.ToInt32(anio));

                foreach (var elemento in uf)
                {
                    Trace.WriteLine(elemento.ToString());
                }

                Assert.AreEqual(uf.ContainsKey(anio), true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido obtener la lista de UF. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"Error de búsqueda. Error: {e}");
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestIndicadorMensual()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            string periodo = Environment.GetEnvironmentVariable("TEST_UF_MES");
            Uf Indicador = new Uf();

            try
            {
                Dictionary<string, object> uf = Indicador.Mensual(periodo);

                foreach (var elemento in uf)
                {
                    Trace.WriteLine(elemento.ToString());
                }

                Assert.AreEqual(uf.ContainsKey(periodo), true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido obtener la lista de UF. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"Error de búsqueda. Error: {e}");
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestIndicadorDiario()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            string fecha = Environment.GetEnvironmentVariable("TEST_UF_FECHA");
            string valor = Environment.GetEnvironmentVariable("TEST_UF_VALOR");
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            Uf Indicador = new Uf();

            try
            {
                // Despliega en float el valor de la UF obtenida de TEST_UF_FECHA.
                float uf = Indicador.Diario(fecha);
                Trace.WriteLine(uf.ToString());
                Assert.AreEqual(uf, valor);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido obtener el valor de la UF. Error: {e}");
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
