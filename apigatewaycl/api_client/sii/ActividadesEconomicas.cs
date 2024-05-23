﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using apigatewaycl.api_client.utils;

/// <summary>
/// Módulo para obtener las actividades económicas del SII.
///
/// Para más información sobre la API, consulte la `documentación completa de Actividades Económicas <https://developers.apigateway.cl/#e64eb128-173a-48c7-ab0b-b6152e59c327>`_.
/// </summary>
namespace apigatewaycl.api_client.sii
{
    public class ActividadesEconomicas : ApiBase
    {
        /// <summary>
        /// Cliente específico para interactuar con los endpoints de actividades económicas de la API de API Gateway.
        /// Provee métodos para obtener listados de actividades económicas, tanto de primera como de segunda categoría.
        /// </summary>
        /// <param name="apiToken" type="string">Token de autenticación para la API.</param>
        /// <param name="apiUrl" type="string">URL base para la API.</param>
        /// <param name="apiVersion" type="string">¿Versión de la API.</param>
        /// <param name="apiRaiseForStatus" type="bool">Si se debe lanzar una excepción automáticamente para respuestas de error HTTP. Por omisión es true.</param>
        /// <param name="kwargs" type="Dictionary<string, string>">Argumentos adicionales para la autenticación.</param>
        public ActividadesEconomicas(string apiToken = null, string apiUrl = null, string apiVersion = null, bool apiRaiseForStatus = true, Dictionary<string, string> kwargs = null)
            : base(apiToken, apiUrl, apiVersion, apiRaiseForStatus, kwargs)
        {

        }

        /// <summary>
        /// Obtiene un listado de actividades económicas. Puede filtrar por categoría.
        /// </summary>
        /// <param name="categoria" type="int">Categoría de las actividades económicas (opcional).</param>
        /// <returns type="Dictionary<string, object>">Respuesta JSON con el listado de actividades económicas.</returns>
        public Dictionary<string, object> ListadoActividades(int categoria = 0)
        {
            string url = "/sii/contribuyentes/actividades_economicas";

            if (categoria >= 1 && categoria <= 2)
            {
                url += $"/{categoria}";
            }

            var response = this.client.Get(url);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
        }

        /// <summary>
        /// Obtiene un listado de actividades económicas de primera categoría.
        /// </summary>
        /// <returns type="List<Dictionary<string, object>>">Dictionary Respuesta JSON con el listado de actividades económicas de primera categoría.</returns>
        public Dictionary<string, object> ListadoPrimeraCategoria()
        {
            return this.ListadoActividades(1);
        }

        /// <summary>
        /// Obtiene un listado de actividades económicas de segunda categoría.
        /// </summary>
        /// <returns type="List<Dictionary<string, object>>">Dictionary Respuesta JSON con el listado de actividades económicas de segunda categoría.</returns>
        public Dictionary<string, object> ListadoSegundaCategoria()
        {
            return this.ListadoActividades(2);
        }
    }
}
