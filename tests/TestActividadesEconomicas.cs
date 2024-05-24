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
        /// Pruebas de ActividadesEconomicas que recibirá de parámetro un entero 1
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// Actividades: Instancia de ActividadesEconomicas
        /// listado: Resultado del método ListadoActividades(1)
        /// 
        /// Assert: listado >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestActividadesPrimera()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
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
        /// Pruebas de ActividadesEconomicas que recibirá de parámetro un entero 2
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// Actividades: Instancia de ActividadesEconomicas
        /// listado: Resultado del método ListadoActividades(2)
        /// 
        /// Assert: listado >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestActividadesSegunda()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
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
        /// Pruebas de ActividadesEconomicas que no recibirá parámetro (default: 0)
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// Actividades: Instancia de ActividadesEconomicas
        /// listado: Resultado del método ListadoActividades()
        /// 
        /// Assert: listado >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestActividadesDefault()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
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
        /// Pruebas de ActividadesEconomicas que hará un listado default (Primera categoría)
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// Actividades: Instancia de ActividadesEconomicas
        /// listado: Resultado del método ListadoPrimeraCategoria()
        /// 
        /// Assert: listado >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestListadoPrimeraCategoria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
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
        /// Pruebas de ActividadesEconomicas que hará un listado default (Segunda categoría)
        /// 
        /// Variables:
        /// test_env: Instancia para inicialización de Variables de entorno
        /// Actividades: Instancia de ActividadesEconomicas
        /// listado: Resultado del método ListadoSegundaCategoria()
        /// 
        /// Assert: listado >= 0 == true
        /// Exception AssertFailedException: Si las condiciones no se cumplen
        /// Exception ApiException: Si otro error es encontrado
        /// </summary>
        [TestMethod]
        public void TestListadoSegundaCategoria()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Cambiar a TestEnv_dist
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
