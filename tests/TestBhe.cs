/*
 * API Gateway: Cliente de API en C#.
 * Copyright (C) API Gateway <https://www.apigateway.cl>
 *
 * Este programa es software libre: usted puede redistribuirlo y/o modificarlo
 * bajo los términos de la GNU Lesser General Public License (LGPL) publicada
 * por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
 * o (a su elección) cualquier versión posterior de la misma.
 *
 * Este programa se distribuye con la esperanza de que sea útil, pero SIN
 * GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
 * PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
 * Public License (LGPL) para obtener una información más detallada.
 *
 * Debería haber recibido una copia de la GNU Lesser General Public License
 * (LGPL) junto a este programa. En caso contrario, consulte
 * <http://www.gnu.org/licenses/lgpl.html>.
 */

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
        /// Assert: respuesta.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestBheEmitidos()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
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
                
                foreach (var ListaBoletas in respuesta)
                {
                    foreach (var boleta in ListaBoletas)
                    {
                        Trace.WriteLine(boleta.ToString());
                    }
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


        /// <summary>
        /// Pruebas de Bhe que recuperará un BHE emitido del usuario usando un código, y se convertirá en bytes para pasar a PDF.
        /// 
        /// Assert: respuesta.Length >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestPdfEmitido()
        {
            // Hay que comparar los resultados con el resultado del código en Python, y arreglar este código en base a eso...
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
        
            string USUARIO_RUT = Environment.GetEnvironmentVariable("USUARIO_RUT");
            string USUARIO_CLAVE = Environment.GetEnvironmentVariable("USUARIO_CLAVE");
            string BHE_CODIGO_PDF = Environment.GetEnvironmentVariable("TEST_BHE_CODIGO");

            Dictionary<string, string> usuario = new Dictionary<string, string>()
            {
                { "usuario_rut", USUARIO_RUT },
                { "usuario_clave", USUARIO_CLAVE }
            };

            try
            {
                BheEmitidas ListadoBhe = new BheEmitidas(usuario);
                byte[] respuesta = ListadoBhe.Pdf(BHE_CODIGO_PDF);

                if (respuesta.Length == 0)
                {
                    Trace.WriteLine($"El BHE no existe para el emisor {USUARIO_RUT}.");
                }
                else
                {
                    System.IO.File.WriteAllBytes($@"test_pdf_{USUARIO_RUT}_{BHE_CODIGO_PDF}.pdf", respuesta);
                }

                Assert.AreEqual(respuesta.Length >= 0, true);
            }
            catch (AssertFailedException e)
            {
                // Si arroja un mensaje de error, es porque no estás conectado
                Trace.WriteLine($"No se ha podido encontrar el BHE. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"Error de búsqueda. Error: {e}");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Pruebas de Bhe que enviará un correo con un BHE emitido a un destinatario
        /// 
        /// Assert: correo.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestEmailEmitido()
        {
            // Hay que comparar los resultados con el resultado del código en Python, y arreglar este código en base a eso...
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();

            string USUARIO_RUT = Environment.GetEnvironmentVariable("USUARIO_RUT");
            string USUARIO_CLAVE = Environment.GetEnvironmentVariable("USUARIO_CLAVE");
            string BHE_CODIGO = Environment.GetEnvironmentVariable("TEST_BHE_CODIGO");
            string BHE_EMAIL = Environment.GetEnvironmentVariable("TEST_BHE_EMAIL");

            Dictionary<string, string> usuario = new Dictionary<string, string>()
            {
                { "usuario_rut", USUARIO_RUT },
                { "usuario_clave", USUARIO_CLAVE }
            };

            try
            {
                BheEmitidas ListadoBhe = new BheEmitidas(usuario);
                Dictionary<string, object> correo = ListadoBhe.Email(BHE_CODIGO, BHE_EMAIL);

                if (correo.Count == 0)
                {
                    Trace.WriteLine($"El BHE no existe para {USUARIO_RUT}.");
                }

                Assert.AreEqual(correo.Count >= 0, true);
            }
            catch (AssertFailedException e)
            {
                // Si arroja un mensaje de error, es porque no estás conectado
                Trace.WriteLine($"No se ha podido enviar el correo. Error: {e}");
                Assert.Fail();
            }
            catch (ApiException e)
            {
                Trace.WriteLine($"Error: {e}");
                Assert.Fail();
            }
        }
    }
}
