// <copyright file="StoreController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Converters;
    using SwaggerPetstore.Standard;
    using SwaggerPetstore.Standard.Authentication;
    using SwaggerPetstore.Standard.Exceptions;
    using SwaggerPetstore.Standard.Http.Client;
    using SwaggerPetstore.Standard.Http.Request;
    using SwaggerPetstore.Standard.Http.Request.Configuration;
    using SwaggerPetstore.Standard.Http.Response;
    using SwaggerPetstore.Standard.Utilities;

    /// <summary>
    /// StoreController.
    /// </summary>
    public class StoreController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        internal StoreController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers)
            : base(config, httpClient, authManagers)
        {
        }

        /// <summary>
        /// Place an order for a pet.
        /// </summary>
        /// <param name="body">Required parameter: order placed for purchasing the pet.</param>
        /// <returns>Returns the Models.Order response from the API call.</returns>
        public Models.Order PlaceOrder(
                Models.Order body)
        {
            Task<Models.Order> t = this.PlaceOrderAsync(body);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Place an order for a pet.
        /// </summary>
        /// <param name="body">Required parameter: order placed for purchasing the pet.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.Order response from the API call.</returns>
        public async Task<Models.Order> PlaceOrderAsync(
                Models.Order body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/store/order");

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
                { "Content-Type", "application/json" },
            };

            // append body params.
            var bodyText = ApiHelper.JsonSerialize(body);

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().PostBody(queryBuilder.ToString(), headers, bodyText);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid Order", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.Order>(response.Body);
        }

        /// <summary>
        /// For valid response try integer IDs with value >= 1 and <= 10. Other values will generated exceptions.
        /// </summary>
        /// <param name="orderId">Required parameter: ID of pet that needs to be fetched.</param>
        /// <returns>Returns the Models.Order response from the API call.</returns>
        public Models.Order GetOrderById(
                long orderId)
        {
            Task<Models.Order> t = this.GetOrderByIdAsync(orderId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// For valid response try integer IDs with value >= 1 and <= 10. Other values will generated exceptions.
        /// </summary>
        /// <param name="orderId">Required parameter: ID of pet that needs to be fetched.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.Order response from the API call.</returns>
        public async Task<Models.Order> GetOrderByIdAsync(
                long orderId,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/store/order/{orderId}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "orderId", orderId },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid ID supplied", context);
            }

            if (response.StatusCode == 404)
            {
                throw new ApiException("Order not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.Order>(response.Body);
        }

        /// <summary>
        /// For valid response try integer IDs with positive integer value. Negative or non-integer values will generate API errors.
        /// </summary>
        /// <param name="orderId">Required parameter: ID of the order that needs to be deleted.</param>
        public void DeleteOrder(
                long orderId)
        {
            Task t = this.DeleteOrderAsync(orderId);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// For valid response try integer IDs with positive integer value. Negative or non-integer values will generate API errors.
        /// </summary>
        /// <param name="orderId">Required parameter: ID of the order that needs to be deleted.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task DeleteOrderAsync(
                long orderId,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/store/order/{orderId}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "orderId", orderId },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Delete(queryBuilder.ToString(), headers, null);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid ID supplied", context);
            }

            if (response.StatusCode == 404)
            {
                throw new ApiException("Order not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Returns a map of status codes to quantities.
        /// </summary>
        /// <returns>Returns the Dictionary of string, int response from the API call.</returns>
        public Dictionary<string, int> GetInventory()
        {
            Task<Dictionary<string, int>> t = this.GetInventoryAsync();
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Returns a map of status codes to quantities.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Dictionary of string, int response from the API call.</returns>
        public async Task<Dictionary<string, int>> GetInventoryAsync(CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/store/inventory");

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Dictionary<string, int>>(response.Body);
        }
    }
}