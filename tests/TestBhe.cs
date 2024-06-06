﻿/*
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
        /// Pruebas de Bhe que obtendrá PDFs de BheEmitidas
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// USUARIO_RUT: RUT del usuario de SII, obtenido de variable de entorno
        /// USUARIO_CLAVE: Clave del usuario de SII, obtenida de variable de entorno
        /// TEST_BHE_CODIGO_PDF: Código del pdf a obtener
        /// usuario: Diccionario que contiene RUT y clave
        /// ListadoBhe: Instancia de BheEmitidas que recibe de parámetros el diccionario usuario
        /// respuesta: Resultado del método Pdf(codigo) en BheEmitidas
        /// 
        /// Assert: respuesta >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestPdfEmitido()
        {
            // Hay que comparar los resultados con el resultado del código en Python, y arreglar este código en base a eso...
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
        
            string USUARIO_RUT = Environment.GetEnvironmentVariable("USUARIO_RUT");
            string USUARIO_CLAVE = Environment.GetEnvironmentVariable("USUARIO_CLAVE");
            string BHE_CODIGO_PDF = Environment.GetEnvironmentVariable("TEST_BHE_CODIGO_PDF");

            Dictionary<string, string> usuario = new Dictionary<string, string>()
            {
                { "usuario_rut", USUARIO_RUT },
                { "usuario_clave", USUARIO_CLAVE }
            };

            try
            {
                BheEmitidas ListadoBhe = new BheEmitidas(usuario);
                byte[] respuesta = ListadoBhe.Pdf(BHE_CODIGO_PDF);

                Trace.WriteLine(ListadoBhe.Pdf(BHE_CODIGO_PDF));
                
                if (respuesta.Length == 0)
                {
                    Trace.WriteLine($"El PDF no existe para el emisor {USUARIO_RUT}.");
                }

                Assert.AreEqual(respuesta.Length >= 0, true);
            }
            catch (AssertFailedException e)
            {
                // Si arroja un mensaje de error, es porque no estás conectado
                Trace.WriteLine($"No se ha podido encontrar el PDF. Error: {e}");
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
