using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using apigatewaycl.api_client.utils;

/// <summary>
/// Módulo para la emisión de Boletas de Terceros Electrónicas del SII.
///
/// Para más información sobre la API, consulte la `documentación completa de las BTE <https://developers.apigateway.cl/#e08f50ab-5509-48ab-81ab-63fc8e5985e1>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    public class BteEmitidas : ApiBase
    {

        /// <summary>
        /// Cliente específico para gestionar Boletas de Terceros Electrónicas (BHE) emitidas.
        /// 
        /// Provee métodos para emitir, anular, y consultar información relacionada con BTEs.
        /// </summary>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales.</param>
        public BteEmitidas(Dictionary<string, string> kwargs)
            : base(kwargs: kwargs)
        {
            
        }

        /// <summary>
        /// Obtiene los documentos de BTE emitidos por un emisor en un periodo específico.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor de las BTE.</param>
        /// <param name="periodo" type="string">Período de tiempo de las BTE emitidas.</param>
        /// <returns type="List<Dictionary<string, object>>">Respuesta JSON con los documentos BTE.</returns>
        public List<Dictionary<string, object>> Documentos(string emisor, string periodo)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var r = this.client.Post($"/sii/bte/emitidas/documentos/{emisor}/{periodo}", body);
            var jsonResponse = r.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResponse);
        }

        /// <summary>
        /// Obtiene la representación HTML de una BTE emitida.
        /// </summary>
        /// <param name="codigo" type="string">Código único de la BTE.</param>
        /// <returns type="string">Contenido HTML de la BTE.</returns>
        public string Html(string codigo)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };

            var r = this.client.Post($"/sii/bte/emitidas/html/{codigo}", body);

            return r.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Emite una nueva Boleta de Tercero Electrónica.
        /// </summary>
        /// <param name="datos" type="Dictionary<string, object>">Datos de la boleta a emitir.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la confirmación de la emisión de la BTE.</returns>
        public Dictionary<string, object> Emitir(Dictionary<string, object> datos)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()},
                {"boleta", datos }
            };

            var response = this.client.Post($"/sii/bte/emitidas/emitir", body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }

        /// <summary>
        /// Anula una BTE emitida.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor de la boleta.</param>
        /// <param name="numero" type="string">Número de la boleta.</param
        /// <param name="causa" type="int">Causa de anulación.</param>
        /// <param name="periodo" type="string">Período de emisión de la boleta (opcional).</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la confirmación de la anulación.</returns>
        public Dictionary<string, object> Anular(string emisor, string numero, int causa = 3, string periodo = null)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };
            string resource = $"/sii/bte/emitidas/anular/{emisor}/{numero}?causa={causa}";

            if (periodo != null || !periodo.Equals(string.Empty))
            {
                resource += $"&periodo={periodo}";
            }

            var response = client.Post(resource, body);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }

        /// <summary>
        /// Obtiene la tasa de retención aplicada a un receptor por un emisor específico.
        /// </summary>
        /// <param name="emisor" type="string">RUT del emisor de la boleta.</param
        /// <param name="receptor" type="string">RUT del receptor de la boleta.</param>
        /// <param name="periodo" type="string">Período de emisión de la boleta (opcional).</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con la tasa de retención.</returns>
        public Dictionary<string, object> ReceptorTasa(string emisor, string receptor, string periodo = null)
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "auth", this.GetAuthPass()}
            };
            string resource = $"/sii/bte/emitidas/anular/{emisor}/{receptor}";
            
            if (periodo != null || !periodo.Equals(string.Empty))
            {
                resource += $"?periodo={periodo}";
            }
            
            var response = client.Get($"/sii/bte/emitidas/receptor_tasa/{emisor}/{receptor}");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }
    }
}
