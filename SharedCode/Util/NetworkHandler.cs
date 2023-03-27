using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SharedCode.Util
{
    public class NetworkErrorException : HttpRequestException
    {
        public int Code;
        public NetworkErrorException(int code)
        {
            this.Code = code;
        }
        public NetworkErrorException(int code, string message) : base(message)
        {
            this.Code = code;
        }
        public NetworkErrorException(int code, string message, Exception inner) : base(message, inner)
        {
            this.Code = code;
        }
    }

    public static class NetworkErrors
    {
        public static readonly NetworkErrorException BadRequest = new NetworkErrorException(400, "Bad request");
        public static readonly NetworkErrorException NotFound = new NetworkErrorException(404, "Not Found");
        public static readonly NetworkErrorException InternalServerError = new NetworkErrorException(500, "Internal Server Error");
        public static readonly NetworkErrorException UnknownError = new NetworkErrorException(0, "Unknown Error");
    }

    public static class NetworkHandler
	{
        private static HttpClient httpClient = new HttpClient();

        private static Uri baseAddress = new Uri("https://pokeapi.co/api/v2/");

		public static async Task<T> GetData<T>(string endpoint)
		{
            httpClient.BaseAddress = baseAddress;
            try
            {
                var response = await httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode >= HttpStatusCode.BadRequest)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            throw NetworkErrors.BadRequest;
                        case HttpStatusCode.NotFound:
                            throw NetworkErrors.NotFound;
                        case HttpStatusCode.InternalServerError:
                            throw NetworkErrors.InternalServerError;
                        default:
                            throw NetworkErrors.UnknownError;
                    }
                } else
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
            }
            catch (NetworkErrorException ex)
            {
                throw ex;
            }
        }
	}
}

