using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apigatewaycl.api_client.utils
{
    public class ApiException : Exception
    {
        public int Code;
        public Dictionary<int, string> Parameters { get; set; }

        /// <summary>
        /// Excepción personalizada para errores en el cliente de la API.
        /// </summary>
        /// <param name="message" type="string">mensaje de error.</param>
        /// <param name="code" type="int">Código de error (opcional).</param>
        /// <param name="parameters" type="Dictionary<int, string>"> parámetros adicionales del error (opcional).</param>
        public ApiException(string message, int code = 0, Dictionary<int, string> parameters = null) : base(message)
        {
            this.Code = code;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Devuelve una representación en cadena del error, proporcionando un contexto claro
        /// del problema ocurrido. Esta representación incluye el prefijo "[API Gateway]",
        /// seguido del código de error si está presente, y el mensaje de error.
        /// 
        /// Si se especifica un código de error, el formato será
        /// "[API Gateway] Error {code}: {message}"
        /// 
        /// Si no se especifica un código de error, el formato será
        /// "[API Gateway] {message}"
        /// </summary>
        /// <returns type="string">Cadena que representa el error de manera clara y concisa.</returns>
        public override string ToString()
        {
            if (this.Code != 0)
            {
                return $"[API Gateway] Error {this.Code}: {this.Message}";
            }
            else
            {
                return $"[API Gateway] {this.Message}";
            }
        }
    }
}
