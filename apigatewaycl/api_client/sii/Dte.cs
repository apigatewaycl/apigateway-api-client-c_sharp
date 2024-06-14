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
using Newtonsoft.Json;
using apigatewaycl.api_client.utils;

/// <summary>
/// Módulo para interactuar con las opciones de Documentos Tributarios Electrónicos (DTE) del SII.
///
/// Para más información sobre la API, consulte la `documentación completa de los DTE <https://developers.apigateway.cl/#8c113b9a-ea05-4981-9273-73e3f20ef991>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    /// <summary>
    /// Cliente específico para interactuar con los endpoints de contribuyentes de la API de API Gateway.
    ///
    /// Proporciona métodos para consultar la autorización de emisión de DTE de un contribuyente.
    /// 
    /// NOTA DEL DESARROLLADOR: Porque ya existe la clase "Contribuyentes", se renombró a "DteContribuyentes".
    /// </summary>
    public class DteContribuyentes : ApiBase
    {
        public DteContribuyentes()
        {
            
        }

        /// <summary>
        /// Verifica si un contribuyente está autorizado para emitir DTE.
        /// </summary>
        /// <param name="rut" type="string">RUT del contribuyente a verificar.</param>
        /// <param name="certificacion" type="bool">Indica si se consulta en ambiente de certificación (opcional).</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con el estado de autorización del contribuyente.</returns>
        public Dictionary<string, object> Autorizacion(string rut, bool certificacion = false)
        {
            int certificacionFlag = certificacion ? 1 : 0;
            var response = this.client.Get($"/sii/dte/contribuyentes/autorizado/{rut}?certificacion={certificacionFlag}");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
    }

    class DteEmitidos : ApiBase
    {
        /// <summary>
        /// Cliente específico para gestionar DTE emitidos.
        ///
        /// Permite verificar la validez y autenticidad de un DTE emitido.
        /// </summary>
        /// <param name="usuarioRut" type="string">RUT del usuario.</param>
        /// <param name="usuarioClave" type="string">Clave del usuario.</param>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales.</param>
        public DteEmitidos(Dictionary<string, string> kwargs)
            : base(kwargs: kwargs)
        {
            
        }

        /// <summary>
        /// Verifica la validez de un DTE emitido.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor del DTE.</param>
        /// <param name="receptor" type="string">RUT del receptor del DTE.</param>
        /// <param name="dte" type="int">Tipo de DTE.</param>
        /// <param name="folio" type="int">Número de folio del DTE.</param>
        /// <param name="fecha" type="string">Fecha de emisión del DTE.</param>
        /// <param name="total" type="int">Monto total del DTE.</param>
        /// <param name="firma" type="string">Firma electrónica del DTE (opcional).</param>
        /// <param name="certificacion", type="bool">Indica si la verificación es en ambiente de certificación (opcional).</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con el resultado de la verificación del DTE.</returns>
        public Dictionary<string, object> Verificar(string emisor, string receptor, int dte, int folio, string fecha, int total, string firma = null, bool certificacion = false)
        {
            Dictionary<string, object> dictDte = new Dictionary<string, object>()
            {
                { "emisor", emisor },
                { "receptor", receptor },
                { "dte", dte },
                { "folio", folio },
                { "fecha", fecha },
                { "total", total },
                { "firma", firma }
            };
            Dictionary<string, object> body = new Dictionary<string, object>()
            {
                { "auth", this.GetAuthPass() },
                { "dte", dictDte }
            };

            int certificacionFlag = certificacion ? 1 : 0;
            var response = this.client.Post($"/sii/dte/emitidos/verificar?certificacion={certificacionFlag}", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
    }
}
