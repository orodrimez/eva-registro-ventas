using RegistroVentas.WinForms.Models;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RegistroVentas.WinForms.Services
{
    public class RegistroVentasApiClient
    {
        private readonly string _baseUrl;
        private readonly JavaScriptSerializer _serializer;

        public RegistroVentasApiClient(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _serializer = new JavaScriptSerializer();
        }

        public Task<TokenResponse> GenerarTokenAsync(string usuario, string rol)
        {
            var request = new TokenRequest
            {
                usuario = usuario,
                rol = rol
            };

            return SendAsync<TokenResponse>("POST", "/auth/token", request, null);
        }

        public async Task<string> CifrarSecretoAsync(string valor)
        {
            var request = new EncryptRequest
            {
                valor = valor
            };

            var response = await SendAsync<EncryptResponse>("POST", "/crypto/encrypt", request, null );

            return response.secreto;
        }

        public Task<RegistroOperacionResponse> RegistrarVentaAsync(string token, string cliente, string importe, string secretoCifrado)
        {
            var request = new RegistrarVentaRequest
            {
                operacion = "venta",
                importe = importe,
                cliente = cliente,
                secreto = secretoCifrado
            };

            return SendAsync<RegistroOperacionResponse>("POST", "/registros", request, token);
        }

        public Task<RegistroOperacionResponse> ObtenerPorPkAsync(string token, int pk)
        {
            return SendAsync<RegistroOperacionResponse>("GET", "/registros/" + pk, null, token);
        }

        public Task<PagedRegistrosResponse> ObtenerPaginadoAsync(string token, int page, int pageSize)
        {
            var path = $"/registros?page={page}&pageSize={pageSize}";

            return SendAsync<PagedRegistrosResponse>("GET", path, null, token);
        }

        public Task<RegistroOperacionResponse> CancelarAsync(string token, string referencia)
        {
            var path = $"/registros/{Uri.EscapeDataString(referencia)}/cancelacion";

            return SendAsync<RegistroOperacionResponse>("PATCH", path, null, token);
        }

        private async Task<T> SendAsync<T>(string method, string path, object body, string bearerToken)
        {
            var url = _baseUrl + path;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json";
            request.ContentType = "application/json";

            if (!string.IsNullOrWhiteSpace(bearerToken))
            {
                request.Headers[HttpRequestHeader.Authorization] = "Bearer " + bearerToken;
            }

            if (body != null)
            {
                var json = _serializer.Serialize(body);
                var bytes = Encoding.UTF8.GetBytes(json);

                request.ContentLength = bytes.Length;

                using (var requestStream = await request.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    var responseBody = await ReadBodyAsync(response);

                    if (string.IsNullOrWhiteSpace(responseBody))
                    {
                        return default(T);
                    }

                    return _serializer.Deserialize<T>(responseBody);
                }
            }
            catch (WebException ex)
            {
                var httpResponse = ex.Response as HttpWebResponse;

                if (httpResponse == null)
                {
                    throw new Exception("Error de comunicación con la API.", ex);
                }

                var errorBody = await ReadBodyAsync(httpResponse);

                throw new ApiException((int)httpResponse.StatusCode, httpResponse.StatusCode.ToString(), errorBody);
            }
        }

        private static async Task<string> ReadBodyAsync(HttpWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                {
                    return string.Empty;
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; private set; }
        public string Status { get; private set; }
        public string Body { get; private set; }

        public ApiException(int statusCode, string status, string body) : 
            base("HTTP " + statusCode + " - " + status)
        {
            StatusCode = statusCode;
            Status = status;
            Body = body;
        }
    }
}