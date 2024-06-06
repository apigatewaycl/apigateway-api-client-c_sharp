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
/// Módulo para obtener los datos a través del SII.
/// 
/// Para más información sobre la API, consulte la `documentación completa de los Contribuyentes <https://developers.apigateway.cl/#c88f90b6-36bb-4dc2-ba93-6e418ff42098>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    /// <summary>
    /// Cliente específico para interactuar con los endpoints de contribuyentes de la API de API Gateway.
    ///
    /// Hereda de ApiBase y utiliza su funcionalidad para realizar solicitudes a la API.
    /// </summary>
    public class Contribuyentes : ApiBase
    {
        public Contribuyentes()
        {

        }
        /// <summary>
        /// Obtiene la situación tributaria de un contribuyente.
        /// </summary>
        /// <param name="rut" type="string">RUT del contribuyente.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la situación tributaria del contribuyente.</returns>
        public Dictionary<string, object> SituacionTributaria(string rut)
        {
            var response = this.client.Get($"/sii/contribuyentes/situacion_tributaria/tercero/{rut}");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }

        /// <summary>
        /// Verifica el RUT de un contribuyente.
        /// </summary>
        /// <param name="serie">Serie del RUT a verificar.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la verificación del RUT.</returns>
        public Dictionary<string, object> VerificarRut(string serie)
        {
            var response = this.client.Get($"/sii/contribuyentes/rut/verificar/{serie}");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
    }
}
