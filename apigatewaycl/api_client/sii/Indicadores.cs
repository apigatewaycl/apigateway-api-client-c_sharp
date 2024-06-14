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
using Newtonsoft.Json;
using apigatewaycl.api_client.utils;

/// <summary>
/// Módulo para obtener indicadores desde el SII.
///
/// Para más información sobre la API, consulte la `documentación completa de los Indicadores <https://developers.apigateway.cl/#65aa568c-4c5a-448b-9f3b-95c3d9153e4d>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    /// <summary>
    /// Cliente específico para interactuar con los endpoints de valores de UF (Unidad de Fomento) de la API de API Gateway.
    ///
    /// Provee métodos para obtener valores de UF anuales, mensuales y diarios.
    /// </summary>
    public class Uf : ApiBase
    {
        public Uf()
        {

        }

        /// <summary>
        /// Obtiene los valores de la UF para un año específico.
        /// </summary>
        /// <param name="anio" type="int">Año para el cual se quieren obtener los valores de la UF.</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con los valores de la UF del año especificado.</returns>
        public Dictionary<string, object> Anual(int anio)
        {
            string anioString = anio.ToString();
            var response = this.client.Get($"/sii/indicadores/uf/anual/{anio}");

            try
            {
                var datos = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(datos);

                return result.ContainsKey(anioString) ? result : new Dictionary<string, object>();
            }
            catch(NullReferenceException e)
            {
                throw new ApiException($"Error de referencia nula: {e}");
            }
        }

        /// <summary>
        /// Obtiene los valores de la UF para un mes específico.
        /// </summary>
        /// <param name="periodo" tyoe="string">Período en formato AAAAMM (año y mes).</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con los valores de la UF del mes especificado.</returns>
        public Dictionary<string, object> Mensual(string periodo)
        {
            string anio = periodo.Substring(0, 4);
            string mes = periodo.Substring(4, 2);
            
            if (anio.All(char.IsDigit) == false)
            {
                throw (new ApiException("Formato de año incorrecto."));
            }
            if (mes.All(char.IsDigit) == false)
            {
                throw (new ApiException("Formato de mes incorrecto."));
            }
            if (Convert.ToInt32(mes) < 1 || Convert.ToInt32(mes) > 12)
            {
                throw (new ApiException("Mes no puede ser mayor que 12 ni menor que 1"));
            }

            var response = this.client.Get($"/sii/indicadores/uf/anual/{anio}/{mes}");
            var datos = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(datos);

            return result.ContainsKey(periodo) ? result : new Dictionary<string, object>();
        }

        /// <summary>
        /// Obtiene el valor de la UF para un día específico.
        /// </summary>
        /// <param name="fecha" type="string">Fecha en formato AAAA-MM-DD.</param>
        /// <returns type="float">Valor de la UF para el día especificado.</returns>
        public float Diario(string fecha)
        {
            string anio = fecha.Split('-')[0];
            string mes = fecha.Split('-')[1];
            string dia = fecha.Split('-')[2];

            var response = this.client.Get($"/sii/indicadores/uf/anual/{anio}/{mes}/{dia}");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            try
            {
                Dictionary<string, float> datos = JsonConvert.DeserializeObject<Dictionary<string, float>>(jsonResponse);
                string indice = $"{anio}{mes}{dia}";

                return datos.ContainsKey(indice) ? datos[indice] : 0.0f;
            } catch (JsonSerializationException e)
            {
                throw (new ApiException($"Error de deserialización Json (Fecha incorrecta): {e}"));
            }
        }

    }
}
