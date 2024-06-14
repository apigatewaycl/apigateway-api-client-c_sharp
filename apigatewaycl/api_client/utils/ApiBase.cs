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

using System.Collections.Generic;

namespace apigatewaycl.api_client.utils
{
    public class ApiBase
    {
        public ApiClient client;

        protected Dictionary<string, Dictionary<string, string>> auth = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Clase base para las clases que consumen la API (wrappers).
        /// </summary>
        /// <param name="apiToken" type="string">Token de autenticación para la API.</param>
        /// <param name="apiUrl" type="string">URL base para la API.</param>
        /// <param name="apiVersion" type="string">¿Versión de la API.</param>
        /// <param name="apiRaiseForStatus" type="bool">Si se debe lanzar una excepción automáticamente para respuestas de error HTTP. Por omisión es true.</param>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales para la autenticación.</param>
        public ApiBase(string apiToken = null, string apiUrl = null, string apiVersion = null, bool apiRaiseForStatus = true, Dictionary<string, string> kwargs = null)
        {
            this.client = new ApiClient(apiToken, apiUrl, apiVersion, apiRaiseForStatus);
            this.SetupAuth(kwargs);
        }

        /// <summary>
        /// Configura la autenticación específica para la aplicación.
        /// </summary>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos clave-valor para configurar la autenticación.</param>
        /// <returns>void</returns>
        private void SetupAuth(Dictionary<string, string> kwargs)
        {
            if (kwargs != null && kwargs.ContainsKey("usuario_rut") && kwargs.ContainsKey("usuario_clave"))
            {
                var usuarioRutObj = kwargs["usuario_rut"];
                var usuarioClaveObj = kwargs["usuario_clave"];
                var usuarioRut = usuarioRutObj?.ToString();
                var usuarioClave = usuarioClaveObj?.ToString();

                if (!string.IsNullOrEmpty(usuarioRut) && !string.IsNullOrEmpty(usuarioClave))
                {
                    this.auth["pass"] = new Dictionary<string, string>
                    {
                        { "rut", usuarioRut },
                        { "clave", usuarioClave }
                    };
                }
            }
        }

        /// <summary>
        /// Obtiene la autenticación de tipo "pass".
        /// </summary>
        /// <returns type="Dictionary<string, Dictionary<string, string>>">Información de autenticación.</returns>
        /// <exception cref="ApiException">Si falta información de autenticación.</exception>
        protected Dictionary<string, Dictionary<string, string>> GetAuthPass()
        {
            if (!this.auth.ContainsKey("pass"))
            {
                throw new ApiException("auth.pass missing.");
            }
            if (this.auth["pass"] == null || !auth["pass"].ContainsKey("rut"))
            {
                throw new ApiException("auth.pass.rut missing.");
            }
            if (string.IsNullOrEmpty(auth["pass"]["rut"]))
            {
                throw new ApiException("auth.pass.rut empty.");
            }
            if (string.IsNullOrEmpty(auth["pass"]["clave"]))
            {
                throw new ApiException("auth.pass.clave empty.");
            }

            return this.auth;
        }
    }
}
