﻿using System;
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
        public static string BaseAddress = "https://pokeapi.co/api/v2/";
        private static HttpClient httpClient = new HttpClient();

		public static async Task<T> GetData<T>(string endpoint)
		{
            var response = new HttpResponseMessage();
            try
            {
                response = await httpClient.GetAsync(endpoint);
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                CheckNetworkException(response);
                throw ex;
            }
        }

        public static async Task<byte[]> LoadImage(string imageUrl)
        {
            try
            {
                Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);
                return await contentsTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private static void CheckNetworkException(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw NetworkErrors.BadRequest;
                case HttpStatusCode.NotFound:
                    throw NetworkErrors.NotFound;
                case var expression when response.StatusCode >= HttpStatusCode.InternalServerError:
                    throw NetworkErrors.InternalServerError;
            }
        }
	}
}

