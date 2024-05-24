using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using apigatewaycl.api_client.sii;
using System.Collections.Generic;
using System.Diagnostics;
using apigatewaycl.api_client.utils;

namespace tests
{
    /// <summary>
    /// Conjunto de pruebas de Contribuyentes
    /// </summary>
    [TestClass]
    public class TestContribuyentes
    {
        /// <summary>
        /// Pruebas de Contribuyentes que obtendrá la situación tributaria de un usuario
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// USUARIO_RUT: RUT del usuario a consultar, obtenido de variable de entorno
        /// Contribuyente: Instancia de Contribuyentes
        /// situacion: Resultado del método SituacionTributaria(rut) en Contribuyentes
        /// 
        /// Assert: situacion > 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestContribuyenteSituacionTributaria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            string USUARIO_RUT = Environment.GetEnvironmentVariable("USUARIO_RUT");
            Contribuyentes Contribuyente = new Contribuyentes();
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
