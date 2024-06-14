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
using apigatewaycl.api_client.utils;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace tests
{
    /// <summary>
    /// Conjunto de pruebas de Indicadores
    /// </summary>
    [TestClass]
    public class TestIndicadores
    {
        /// <summary>
        /// Pruebas de Indicadores que obtendrá los valores de la UF según años determinadas
        /// 
        /// Assert: uf.ContainsKey(anio) == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
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

                foreach (var mes in uf)
                {
                    Trace.WriteLine(mes.ToString());
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

        /// <summary>
        /// Pruebas de Indicadores que obtendrá los valores de la UF según meses determinados
        /// 
        /// Assert: uf.ContainsKey(periodo) == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
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

                foreach (var dia in uf)
                {
                    Trace.WriteLine(dia.ToString());
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

        /// <summary>
        /// Pruebas de Indicadores que obtendrá los valores de la UF en un día específico
        /// 
        /// Assert: uf.ToString() == TEST_UF_VALOR
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestIndicadorDiario()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            string fecha = Environment.GetEnvironmentVariable("TEST_UF_FECHA");
            string valor = Environment.GetEnvironmentVariable("TEST_UF_VALOR");
            Uf Indicador = new Uf();

            try
            {
                // Despliega en float el valor de la UF obtenida de TEST_UF_FECHA.
                float uf = Indicador.Diario(fecha);
                Trace.WriteLine(uf.ToString());
                Assert.AreEqual(uf.ToString(), valor);
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
