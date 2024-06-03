using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using apigatewaycl.api_client.utils;
using System.Diagnostics;

/// <summary>
/// Módulo para interactuar con Boletas de Honorarios Electrónicas, tanto emitidas como recibidas, del SII.
///
/// Para más información sobre la API, consulte la `documentación completa de las BHE <https://developers.apigateway.cl/#4df9775f-2cd3-4b35-80a5-373f2501230c>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    public class BheEmitidas : ApiBase
    {

        // Constantes revisadas
        // Quién debe hacer la retención asociada al honorario para pagar al SII
        public const int RETENCION_RECEPTOR = 1;
        public const int RETENCION_EMISOR = 2;

        // Posibles motivos de anulación de una BHE
        public const int ANULACION_CAUSA_SIN_PAGO = 1;
        public const int ANULACION_CAUSA_SIN_PRESTACION = 2;
        public const int ANULACION_CAUSA_ERROR_DIGITACION = 3;

        /// <summary>
        /// Cliente específico para gestionar Boletas de Honorarios Electrónicas (BHE) emitidas.
        /// 
        /// Provee métodos para emitir, anular, y consultar información relacionada con BHEs.
        /// </summary>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales.</param>
        public BheEmitidas(Dictionary<string, string> kwargs)
            : base(kwargs: kwargs)
        {

        }

        /// <summary>
        /// Obtiene los documentos de BHE emitidos por un emisor en un periodo específico.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor de las boletas.</param>
        /// <param name="periodo" type="string">Período de tiempo de las boletas emitidas, formato AAAAMM.</param>
        /// <returns type="List<Dictionary<string, object>>">Respuesta JSON con los documentos de BHE.</returns>
        public List<Dictionary<string, object>> Documentos(string emisor, string periodo)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var r = this.client.Post($"/sii/bhe/emitidas/documentos/{emisor}/{periodo}", body);
            var jsonResponse = r.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResponse);
        }

        /*
        /// <summary>
        /// Emite una nueva Boleta de Honorarios Electrónica.
        /// </summary>
        /// <param name="boleta" type="Dictionary<string, object>">Información detallada de la boleta a emitir.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la confirmación de la emisión de la BHE.</returns>
        public Dictionary<string, object> Emitir(Dictionary<string, object> boleta)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()},
                { "boleta", boleta}
            };

            var response = this.client.Post("/sii/bhe/emitidas/emitir", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
        */

        /// <summary>
        /// Obtiene el PDF de una BHE emitida.
        /// </summary>
        /// <param name="codigo" type="string">Código único de la BHE.</param>
        /// <returns type="byte[]">Contenido del PDF de la BHE.</returns>
        public byte[] Pdf(string codigo) // BYTE, O BYTE ARRAY??
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var response = this.client.Post($"/sii/bhe/emitidas/pdf/{codigo}", body);
            Trace.WriteLine(response);
            return response.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// Envía por correo electrónico una BHE emitida.
        /// </summary>
        /// <param name="codigo" type="string">Código único de la BHE a enviar.</param>
        /// <param name="email" type="string">Dirección de correo electrónico a la cual enviar la BHE.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la confirmación del envío del email.</returns>
        public Dictionary<string, object> Email(string codigo, string email)
        {
            Dictionary<string, string> correo = new Dictionary<string, string>()
            {
                {"email", email }
            };
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()},
                { "destinatario", correo}
            };

            var response = this.client.Post($"/sii/bhe/emitidas/email/{codigo}", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }

        /*
        /// <summary>
        /// Anula una BHE emitida.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor de la boleta.</param>
        /// <param name="folio" type="string">Número de folio de la boleta.</param>
        /// <param name="causa" type="int">Motivo de la anulación de la boleta.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la confirmación de la anulación de la BHE.</returns>
        public Dictionary<string, object> Anular(string emisor, string folio, int causa = ANULACION_CAUSA_ERROR_DIGITACION)
        {
            // NO SE PUEDE PROBAR
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var response = this.client.Post($"/sii/bhe/emitidas/anular/{emisor}/{folio}?causa={causa}", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }*/
    }

    public class BheRecibidas : ApiBase
    {
        /// <summary>
        /// Cliente específico para gestionar Boletas de Honorarios Electrónicas (BHE) emitidas.
        /// 
        /// Provee métodos para emitir, anular, y consultar información relacionada con BHEs.
        /// </summary>
        /// <param name="usuarioRut" type="string">String RUT del usuario.</param>
        /// <param name="usuarioClave" type="string">String Clave del usuario.(opcional).</param>
        /// <param name="kwargs" type="string">Dictionary Argumentos adicionales.</param>
        public BheRecibidas(Dictionary<string, string> kwargs)
            : base(kwargs: kwargs)
        {
            
        }

        /// <summary>
        /// Obtiene los documentos de BHE recibidos por un receptor en un periodo específico.
        /// </summary>
        /// <param name="receptor" type="string">RUT del receptor de las boletas.</param>
        /// <param name="periodo" type="string">Período de tiempo de las boletas recibidas.</param>
        /// <param name="pagina" type="int">Número de página para paginación (opcional).</param>
        /// <param name="pagina_sig_codigo" type="string">Código para la siguiente página (opcional).</param>
        /// <returns type="List<Dictionary<string, object>>">Respuesta JSON con los documentos de BHE.</returns>
        public List<Dictionary<string, object>> Documentos(string receptor, string periodo, int pagina = 0, string pagina_sig_codigo = null)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };
            string url = $"/sii/bhe/recibidas/documentos/{receptor}/{periodo}";

            if (pagina > 0)
            {
                url += $"?pagina={pagina}&pagina_sig_codigo={pagina_sig_codigo ?? "00000000000000"}";
            }

            var r = this.client.Post(url, body);
            var jsonResponse = r.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResponse);
        }

        /// <summary>
        /// Obtiene el PDF de una BHE recibida.
        /// </summary>
        /// <param name="codigo" type="string">Código único de la BHE.</param>
        /// <returns type="byte">Contenido del PDF de la BHE.</returns>
        public byte[] Pdf(string codigo) // BYTE, O BYTE ARRAY??
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var response = this.client.Post($"/sii/bhe/recibidas/pdf/{codigo}", body);

            return response.Content.ReadAsByteArrayAsync().Result;
        }

        /*
        /// <summary>
        /// Marca una observación en una BHE recibida.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor de la boleta.</param>
        /// <param name="numero" type="string">Número de la boleta.</param>
        /// <param name="causa" type="int">Motivo de la observación.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la confirmación de la observación.</returns>
        public Dictionary<string, object> Observar(string emisor, string numero, int causa = 1)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var response = this.client.Post($"/sii/bhe/recibidas/observar/{emisor}/{numero}?causa={causa}", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
        */
    }
}
