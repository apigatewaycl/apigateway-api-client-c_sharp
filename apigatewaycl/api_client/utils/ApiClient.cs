using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace apigatewaycl.api_client.utils
{
    public class ApiClient
    {
        private const string DEFAULT_URL = "https://apigateway.cl";
        private const string DEFAULT_VERSION = "v1";

        private string token;
        private string url;
        private Dictionary<string, string> headers { get; set; }
        private string version { get; set; }
        private bool raiseForStatus { get; set; }
        private HttpClient httpClient;

        /// <summary>
        /// Cliente para interactuar con la API de API Gateway.
        /// </summary>
        /// <param name="token" type="string">Token de autenticación del usuario. Si no se proporciona, se intentará obtener una variable de entorno.</param>
        /// <param name="url" type="string">URL base de la API. Si no se proporciona, se usará la versión por omisión.</param>
        /// <param name="version" type="string">Versión de la API. Si no se proporciona, se usará la versión por omisión.</param>
        /// <param name="raise_for_status" type="bool">Si se debe lanzar una excepción automáticamente para respuestas de error HTTP. Por omisión es true.</param>
        public ApiClient(string token = null, string url = null, string version = null, bool raiseForStatus = true)
        {
            this.token = this.ValidateToken(token);
            this.url = this.ValidateUrl(url);
            this.headers = this.GenerateHeaders();
            this.version = version ?? DEFAULT_VERSION;
            this.raiseForStatus = raiseForStatus;
            this.httpClient = new HttpClient();
        }

        /// <summary>
        /// Valida y retorna el token de autenticación.
        /// </summary>
        /// <param name="token" type="string">¿Token de autenticación a validar.</param>
        /// <returns type="string">¿Token validado.</returns>
        /// <exception cref="ApiException">Si el token no es válido o está ausente.</exception>
        private string ValidateToken(string token)
        {
            token = token ?? Environment.GetEnvironmentVariable("APIGATEWAY_API_TOKEN");

            if (string.IsNullOrEmpty(token))
            {
                throw new ApiException("Se debe configurar la variable de entorno: APIGATEWAY_API_TOKEN.");
            }

            return token.Trim();
        }

        /// <summary>
        /// URL a validar.
        /// </summary>
        /// <param name="url" type="string">¿URL validada.</param>
        /// <returns type="string">URL validada.</returns>
        /// <exception cref="ApiException">Si la URL no es válida o está ausente.</exception>
        private string ValidateUrl(string url)
        {
            url = url ?? Environment.GetEnvironmentVariable("APIGATEWAY_API_URL") ?? DEFAULT_URL;

            return url.Trim();
        }

        /// <summary>
        /// Genera y retorna las cabeceras por omisión para las solicitudes.
        /// </summary>
        /// <returns type="Dictionary<string, string>">Dictionary Cabeceras por omisión.</returns>
        private Dictionary<string, string> GenerateHeaders()
        {
            return new Dictionary<string, string>
            {
                {"User-Agent", "API-Gateway-Cliente-de-API-en-C#."},
                {"ContentType", "application/json"},
                {"Accept", "application/json"},
                {"Authorization", $"Bearer {this.token}"}
            };
        }

        // Pendiente de revisión
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private StringContent SerializeData(object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Verifica la respuesta de la solicitud HTTP y maneja los errores.
        /// </summary>
        /// <param name="response" type="HTTPResponseMessage"> Objeto de respuesta de requests.</param>
        /// <returns type="HTTPResponseMessage"> Respuesta de la solicitud.</returns>
        /// <exception cref="ApiException">Si la respuesta contiene un error HTTP.</exception>
        private HttpResponseMessage CheckAndReturnResponse(HttpResponseMessage response)
        {
            if ((int)response.StatusCode != 200 && raiseForStatus)
            {
                string errorMessage = response.Content.ReadAsStringAsync().Result;
                throw new ApiException($"Error HTTP: {errorMessage}");
            }
            
            return response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Método privado para realizar solicitudes HTTP.
        /// </summary>
        /// <param name="method" type="HttpMethod">HttpMethod Método HTTP a utilizar.</param>
        /// <param name="resource" type="string">Recurso de la API a solicitar.</param>
        /// <param name="data" type="Dictionary<string, Dictionary<string, object>">Dictionary Datos a enviar en la solicitud (opcional).</param>
        /// <param name="headers" type="Dictionary<string, string>">Dictionary Cabeceras adicionales para la solicitud (opcional).</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        /// <exception cref="ApiException">Si el método HTTP no es soportado o si hay un error de conexión.</exception>
        private HttpResponseMessage SendRequest(HttpMethod method, string resource, Dictionary<string, object> data = null, Dictionary<string, string> headers = null)
        {
            string apiPath = $"/api/{this.version}{resource}";
            Uri fullUrl = new Uri($"{this.url}{apiPath}");
            HttpRequestMessage request = new HttpRequestMessage(method, fullUrl);

            if (data != null)
            {
                request.Content = this.SerializeData(data);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            foreach (var header in this.headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            
            try
            {
                var response = this.httpClient.SendAsync(request).Result;
                HttpResponseMessage resp = this.CheckAndReturnResponse(response);
                
                return resp;
            }
            catch (HttpRequestException e)
            {
                throw new ApiException($"Error en la solicitud: {e}");
            }
            catch (TimeoutException e)
            {
                throw new ApiException($"Error de timeout: {e}");
            }
            catch (Exception e)
            {
                throw new ApiException($"Error: {e}");
            }
        }

        /// <summary>
        /// Realiza una solicitud GET a la API.
        /// </summary>
        /// <param name="resource" type="string">Recurso de la API a solicitar.</param>
        /// <param name="headers" type="Dictionary<string, string>">Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Get(string resource, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Get, resource: resource, headers: headers);
        }

        /// <summary>
        /// Realiza una solicitud DELETE a la API.
        /// </summary>
        /// <param name="resource" type="string">Recurso de la API a solicitar.</param>
        /// <param name="headers" type="Dictionary<string, string>">Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Delete(string resource, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Delete, resource: resource, headers: headers);
        }

        /// <summary>
        /// Realiza una solicitud POST a la API.
        /// </summary>
        /// <param name="resource" type="string">String Recurso de la API a solicitar.</param>
        /// <param name="data" type="Dictionary<string, object>">Datos a enviar en la solicitud.</param>
        /// <param name="headers" type="Dictionary<string, string>">Dictionary Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Post(string resource, Dictionary<string, object> data = null, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Post, resource: resource, data: data, headers: headers);
        }

        /// <summary>
        /// Realiza una solicitud PUT a la API.
        /// </summary>
        /// <param name="resource" type="string">String Recurso de la API a solicitar.</param>
        /// <param name="data" type="Dictionary<string, object>">Datos a enviar en la solicitud.</param>
        /// <param name="headers" type="Dictionary<string, string>">Dictionary Cabeceras adicionales para la solicitud.</param>
        /// <returns type="HttpResponseMessage">Respuesta de la solicitud.</returns>
        public HttpResponseMessage Put(string resource, Dictionary<string, object> data = null, Dictionary<string, string> headers = null)
        {
            return this.SendRequest(method: HttpMethod.Put, resource: resource, data: data, headers: headers);
        }
    }
}
