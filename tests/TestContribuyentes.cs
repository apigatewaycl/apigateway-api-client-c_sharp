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
        /// Assert: situacion.Count > 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestContribuyenteSituacionTributaria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
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
