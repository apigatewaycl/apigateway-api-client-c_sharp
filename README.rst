API Gateway: Cliente de API en C#
=====================================




Cliente para realizar la integración con los servicios web de `API Gateway <https://www.apigateway.cl>`_ desde C#.

Instalación y actualización
---------------------------

Puedes actualizar usando el administrador de paquetes de NuGet, o a través
de la consola.

Instalar usando el Administrador de Paquetes de NuGet:

1.  Crea o abre su proyecto en Visual Studio.

2.  Seleccione en la barra superior:
    Herramientas, Administrador de paquetes NuGet, Administrar paquetes
    NuGet para la solución...

3.  Una vez que cargue la pestaña NuGet - Solución, Seleccione Examinar, y 
    busque "API Gateway".

4.  Seleccione el proyecto en que quiera integrar API Gateway C#, y haga 
    click en "Instalar".

Autenticación en API Gateway
----------------------------

Lo más simple, y recomendado, es usar una variable de entorno con el
`token del usuario <https://apigateway.cl/dashboard#api-auth>`_, la cual será
reconocida automáticamente por el cliente:

.. code:: shell

    export APIGATEWAY_API_TOKEN = "aquí-tu-token-de-usuario"

Si no se desea usar una variable de entorno, al instanciar los objetos se
deberá indicar el token del usuario. Ejemplo con el cliente genérico:

.. code:: C#

    using apigatewaycl;

    string APIGATEWAY_API_TOKEN = "aquí-tu-token-de-usuario";
    Environment.SetEnvironmentVariable("APIGATEWAY_API_TOKEN", APIGATEWAY_API_TOKEN);
    client = apigatewaycl.api_client.utils.ApiClient(APIGATEWAY_API_TOKEN);

El siguiente es un ejemplo con el cliente específico para BHE. Primero se pasan
los datos obligatorios de RUT y clave del usuario. Luego además se pasa el token
delusuario de la API.

.. code:: C#

    using System.Collections.Generic;
    using apigatewaycl.api_client.sii;


    string APIGATEWAY_API_TOKEN = "aquí-tu-token-de-usuario";
    Environment.SetEnvironmentVariable("APIGATEWAY_API_TOKEN, APIGATEWAY_API_TOKEN);
    string SII_USUARIO_RUT = "";
    string SII_USUARIO_CLAVE = "";
    Dictionary<string, string> usuario = new Dictionary<string, string>()
    {
        { "usuario_rut", SII_USUARIO_RUT },
        { "usuario_clave", SII_USUARIO_CLAVE }
    };
    BheEmitidas ListadoBhe = new BheEmitidas(usuario);

Si se usan variables de entorno, en ambos ejemplos se puede omitir el argumento `api_token`.

Licencia
--------

Este programa es software libre: usted puede redistribuirlo y/o modificarlo
bajo los términos de la GNU Lesser General Public License (LGPL) publicada
por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
o (a su elección) cualquier versión posterior de la misma.

Este programa se distribuye con la esperanza de que sea útil, pero SIN
GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
Public License (LGPL) para obtener una información más detallada.

Debería haber recibido una copia de la GNU Lesser General Public License
(LGPL) junto a este programa. En caso contrario, consulte
`GNU Lesser General Public License <http://www.gnu.org/licenses/lgpl.html>`_.

Enlaces
-------

- `Sitio web API Gateway <https://www.apigateway.cl>`_.
- `Código fuente en GitHub <https://github.com/apigatewaycl/apigateway-api-client-c_sharp>`_.
