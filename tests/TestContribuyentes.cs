using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using apigatewaycl.api_client.sii;
using System.Collections.Generic;
using System.Diagnostics;
using apigatewaycl.api_client.utils;

namespace tests
{
    [TestClass]
    public class TestContribuyentes
    {
        
        [TestMethod]
        public void TestContribuyenteSituacionTributaria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            Contribuyentes Contribuyente = new Contribuyentes();

            string USUARIO_RUT = Environment.GetEnvironmentVariable("USUARIO_RUT");
            // Extrae la información del rut exitosamente.

            try
            {
                Dictionary<string, object> situacion = Contribuyente.SituacionTributaria(USUARIO_RUT);
                
                foreach (var elemento in situacion)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                
                Assert.AreEqual(situacion.Count > 0, true);
            }
            catch (AssertFailedException e)
            {
                Trace.WriteLine($"No se ha podido obtener situación económica. Error: {e}");
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
