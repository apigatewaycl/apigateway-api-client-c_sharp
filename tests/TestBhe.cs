using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using apigatewaycl.api_client.sii;
using apigatewaycl.api_client.utils;

namespace tests
{
    /// <summary>
    /// Conjuntos de pruebas de BHE
    /// </summary>
    [TestClass]
    public class TestBhe
    {
        /// <summary>
        /// Pruebas de Bhe que obtendrá documentos de BheEmitidas
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// USUARIO_RUT: RUT del usuario de SII, obtenido de variable de entorno
        /// USUARIO_CLAVE: Clave del usuario de SII, obtenida de variable de entorno
        /// TEST_BHE_FECHA: Fecha de prueba, obtenida de variable de entorno
        /// usuario: Diccionario que contiene RUT y clave
        /// ListadoBhe: Instancia de BheEmitidas que recibe de parámetros el diccionario usuario
        /// respuesta: Resultado del método Documentos(rut, fecha) en BheEmitidas
        /// 
        /// Assert: respuesta >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestBheEmitidos()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();

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
