==============================================================================================================
Manual de Pruebas API Gateway cl
==============================================================================================================

Proyecto: API Gateway
Lenguaje: C#
Versión: v1
Fecha de creación: 17-05-2024
Autor: Santiago Fiol Stephens

==============================================================================================================
TestEnv.cs y Env.cs
==============================================================================================================

Resumen

Ambos archivos contienen las variables del ambiente de prueba.
Si desea utilizar Variables de Entorno propias, puede modificar TestEnv.cs, o redirigir las pruebas a Env.cs.
TestEnv.cs está en el proyecto "tests", mientras que Env.cs está en "apigatewaycl".
Por omisión se está utilizando TestEnv.cs.

SetVariablesDeEntorno()

Define las variables de entorno a utilizar.

-API_GATEWAY_API_TOKEN: Token de autenticación, requerido para probar o utilizar la aplicación.
-API_GATEWAY_API_URL: URL de la API, predefinido.
-TEST_CONTRIBUYENTE_RUT: RUT de contribuyente, necesario para probar o usar BheEmitidas, y otras 
funcionalidades.
-TEST_CONTRIBUYENTE_CLAVE: Clave de SII del contribuyente, necesario en conjunto con el RUT de contribuyente.

==============================================================================================================
TestActividadesEconomicas.cs
==============================================================================================================

Resumen

Archivo de prueba del módulo ActividadesEconomicas.cs que permite ejecutar pruebas unitarias en los casos
que aparecen en el código.

TestActividadesPrimera()

Prueba el método ListadoActividades() usando un entero 1 como parámetro.
Genera un objeto de TestEnv, ejecuta SetVariablesDeEntorno, genera un objeto de ActividadesEconomicas,
y comprueba que no esté vacío.

Parámetros:
-test_env: Objeto de TestEnv, para inicializar variables de entorno
-Actividades: Objeto de ActividadesEconomicas, para ejecutar pruebas en él

Retorna una lista de actividades de categoría 1.

TestActividadesSegunda()

Prueba el método ListadoActividades() usando un entero 2 como parámetro.
Genera un objeto de TestEnv, ejecuta SetVariablesDeEntorno, genera un objeto de ActividadesEconomicas,
y comprueba que no esté vacío.

Retorna una lista de actividades de categoría 2.

TestActividadesDefault()

Prueba el método ListadoActividades() sin un parámetro. Al hacer esto usará el valor por default, 0.
Genera un objeto de TestEnv, ejecuta SetVariablesDeEntorno, genera un objeto de ActividadesEconomicas,
y comprueba que no esté vacío.

Retorna una lista de actividades sin usar las categorías.

TestListadoPrimeraCategoria()

Prueba el método Listado_primera_categoria, el cual generará un diccionario con todas las actividades
de primera categoría. Esto es lo mismo que TestActividadesPrimera().

Retorna una lista de actividades de primera categoría.

TestListadoSegundaCategoria()

Prueba el método Listado_segunda_categoria, el cual generará un diccionario con todas las actividades
de segunda categoría. Esto es lo mismo que TestActividadesSegunda().

Retorna una lista de actividades de segunda categoría.

