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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using apigatewaycl.api_client.utils;

/// <summary>
/// Módulo para interactuar con la sección MiSii de un contribuyente en el sitio web del SII.
///
/// Para más información sobre la API, consulte la `documentación completa de MiSii <https://developers.apigateway.cl/#b585f374-f106-46a9-9f47-666d478b8ac8>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    public class MisiiContribuyentes : ApiBase
    {
        /// <summary>
        /// Cliente específico para interactuar con los endpoints de un Contribuyente de MiSii de la API de API Gateway.
        ///
        /// Hereda de ApiBase y utiliza su funcionalidad para realizar solicitudes a la API.
        /// </summary>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales.</param>
        public MisiiContribuyentes(Dictionary<string, string> kwargs)
            : base(kwargs: kwargs)
        {
            
        }

        /// <summary>
        /// Obtiene los datos de MiSii del contribuyente autenticado en el SII.
        /// </summary>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con los datos del contribuyente.</returns>
        public Dictionary<string, object> MiSiiDatos()
        {
            Dictionary<string, object> body = new Dictionary<string, object>()
            {
                { "auth", this.GetAuthPass() }
            };

            var response = this.client.Post("/sii/misii/contribuyentes/datos", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
    }
}
