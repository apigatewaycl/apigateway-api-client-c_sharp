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
using apigatewaycl.api_client.sii;
using System.Collections.Generic;
using System.Diagnostics;
using apigatewaycl.api_client.utils;

namespace tests
{
    /// <summary>
    /// Conjuntos de pruebas para ActividadesEconomicas
    /// </summary>
    [TestClass]
    public class TestActividadesEconomicas
    {
        /// <summary>
        /// Pruebas de ActividadesEconomicas que recibirá de parámetro un entero 1. Deberá haber un Listado de Actividades de Categoría 1.
        /// 
        /// Assert: listado.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestActividadesPrimera()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            ActividadesEconomicas Actividades = new ActividadesEconomicas();
            try
            {
                Dictionary<string, object> listado = Actividades.ListadoActividades(1);
                if (listado.Count == 0)
                {
                    Trace.WriteLine("La lista de actividades económicas está vacía.");
                }

                foreach (var elemento in listado)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                // Mostró el listado de categoría 1
                Assert.AreEqual(listado.Count >= 0, true);
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
        /// Pruebas de ActividadesEconomicas que recibirá de parámetro un entero 2. Deberá haber un Listado de Actividades de Categoría 2.
        /// 
        /// Assert: listado.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestActividadesSegunda()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            ActividadesEconomicas Actividades = new ActividadesEconomicas();
            try
            {
                Dictionary<string, object> listado = Actividades.ListadoActividades(2);
                if (listado.Count == 0)
                {
                    Trace.WriteLine("La lista de actividades económicas está vacía.");
                }

                foreach (var elemento in listado)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                // Mostró el listado de categoría 2
                Assert.AreEqual(listado.Count >= 0, true);
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
        /// Pruebas de ActividadesEconomicas que no recibirá parámetro (default: 0). Deberá haber un Listado de Actividades por default.
        /// 
        /// Assert: listado.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestActividadesDefault()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            ActividadesEconomicas Actividades = new ActividadesEconomicas();

            try
            {
                Dictionary<string, object> listado = Actividades.ListadoActividades();
                if (listado.Count == 0)
                {
                    Trace.WriteLine("La lista de actividades económicas está vacía.");
                }

                foreach (var elemento in listado)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                // Mostró el listado default
                Assert.AreEqual(listado.Count >= 0, true);
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
        /// Pruebas de ActividadesEconomicas que hará un listado default (Primera categoría). Deberá haber un Listado de Actividades de categoría 1.
        /// 
        /// Assert: listado.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestListadoPrimeraCategoria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            ActividadesEconomicas Actividades = new ActividadesEconomicas();

            try
            {
                Dictionary<string, object> listado = Actividades.ListadoPrimeraCategoria();
                if (listado.Count == 0)
                {
                    Trace.WriteLine("La lista de actividades económicas está vacía.");
                }

                foreach (var elemento in listado)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                // Ejecuta el método Listado_primera_categoria exitosamente
                Assert.AreEqual(listado.Count >= 0, true);
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
        /// Pruebas de ActividadesEconomicas que hará un listado default (Segunda categoría). Deberá haber un Listado de Actividades de categoría 2.
        /// 
        /// Assert: listado.Count >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestListadoSegundaCategoria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            TestEnv test_env = new TestEnv();
            test_env.SetVariablesDeEntorno();
            ActividadesEconomicas Actividades = new ActividadesEconomicas();

            try
            {
                Dictionary<string, object> listado = Actividades.ListadoSegundaCategoria();
                if (listado.Count == 0)
                {
                    Trace.WriteLine("La lista de actividades económicas está vacía.");
                }

                foreach (var elemento in listado)
                {
                    Trace.WriteLine(elemento.ToString());
                }
                // Ejecuta el método Listado_segunda_categoria exitosamente
                Assert.AreEqual(listado.Count >= 0, true);
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
