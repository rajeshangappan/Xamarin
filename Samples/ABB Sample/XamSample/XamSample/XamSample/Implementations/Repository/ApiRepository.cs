using Newtonsoft.Json;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamSample.Contracts;

namespace XamSample.Implementations
{
    /// <summary>
    /// Defines the <see cref="ApiRepository" />.
    /// </summary>
    public class ApiRepository : IApiRepository
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _httpClient.
        /// </summary>
        private HttpClientService _httpClient;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRepository"/> class.
        /// </summary>
        /// <param name="httpClient">The httpClient<see cref="HttpClientService"/>.</param>
        public ApiRepository(HttpClientService httpClient)
        {
            _httpClient = httpClient;
            AddHeader();
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The AddHeader.
        /// </summary>
        private void AddHeader()
        {
            if (Application.Current.Properties.ContainsKey("token") && Application.Current.Properties["token"] != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["token"].ToString());
            }
        }

        /// <summary>
        /// The GetAsync.
        /// </summary>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        private async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await Policy
                .Handle<WebException>(ex =>
                {
                    Trace.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                    return true;
                })
                .WaitAndRetryAsync
                (
                    1,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(5, retryAttempt))
                )
                .ExecuteAsync(async () => await _httpClient.GetAsync(uri));
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The DeleteAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="object"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        public Task<T> DeleteAsync<T>(string uri, object data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        public async Task<T> GetAsync<T>(string uri)
        {
            var jsonResult = string.Empty;
            var logMessage = string.Empty;
            var httpResponseMessage = await GetAsync(uri);
            if (httpResponseMessage.StatusCode == HttpStatusCode.Redirect)
            {
                // await HandleRedirectAsync(httpResponseMessage, uri);

                //Now Retry the original Uri
                httpResponseMessage = await GetAsync(uri);
            }
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                jsonResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            else
            {
                jsonResult = httpResponseMessage.ReasonPhrase;
                logMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            throw new Exception("Error in API");
        }

        /// <summary>
        /// The PostAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="object"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        public async Task<T> PostAsync<T>(string uri, object data)
        {
            HttpContent content = null;

            var inputData = JsonConvert.SerializeObject(data);
            content = new StringContent(inputData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            string jsonResult = string.Empty;
            string logMessage = string.Empty;
            try
            {
                var httpResponseMessage = await _httpClient.PostAsync(uri, content);// await Policy
                                                                                    //.Handle<WebException>(ex =>
                                                                                    //{
                                                                                    //    Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                                                                                    //    return true;
                                                                                    //})
                                                                                    //.WaitAndRetryAsync
                                                                                    //(
                                                                                    //    1,
                                                                                    //    retryAttempt => TimeSpan.FromSeconds(Math.Pow(5, retryAttempt))
                                                                                    //)
                                                                                    //.ExecuteAsync(async () => await _httpClient.PostAsync(uri, content));

                if (httpResponseMessage.StatusCode == HttpStatusCode.Redirect)
                {
                    //await HandleRedirectAsync(httpResponseMessage, uri);
                    //Now Retry the original Uri
                    httpResponseMessage = await _httpClient.PostAsync(uri, content);
                }

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }
                else
                {
                    jsonResult = httpResponseMessage.ReasonPhrase;
                    logMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(jsonResult);
                }
            }
            catch (Exception)
            {

            }
            throw new Exception(jsonResult + logMessage);
        }

        /// <summary>
        /// The PostAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <param name="data">The data<see cref="T"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        public async Task<T> PostAsync<T>(string uri, T data)
        {
            var inputData = JsonConvert.SerializeObject(data);
            var content = new StringContent(inputData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string jsonResult = string.Empty;
            string logMessage = string.Empty;
            var httpResponseMessage = await Policy
                .Handle<WebException>(ex =>
                {
                    Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                    return true;
                })
                .WaitAndRetryAsync
                (
                    1,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(5, retryAttempt))
                )
                .ExecuteAsync(async () => await _httpClient.PostAsync(uri, content));

            if (httpResponseMessage.StatusCode == HttpStatusCode.Redirect)
            {
                // await HandleRedirectAsync(httpResponseMessage, uri);
                //Now Retry the original Uri
                httpResponseMessage = await _httpClient.PostAsync(uri, content);
            }

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                jsonResult = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            else
            {
                jsonResult = httpResponseMessage.ReasonPhrase;
                logMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden ||
                httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception(jsonResult);
            }

            throw new Exception(httpResponseMessage.StatusCode + jsonResult + logMessage);
        }

        #endregion
    }
}
