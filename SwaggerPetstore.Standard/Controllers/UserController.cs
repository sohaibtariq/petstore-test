// <copyright file="UserController.cs" company="APIMatic">
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
    /// UserController.
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        internal UserController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers)
            : base(config, httpClient, authManagers)
        {
        }

        /// <summary>
        /// Creates list of users with given input array.
        /// </summary>
        /// <param name="body">Required parameter: List of user object.</param>
        public void CreateUsersWithArrayInput(
                List<Models.User> body)
        {
            Task t = this.CreateUsersWithArrayInputAsync(body);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Creates list of users with given input array.
        /// </summary>
        /// <param name="body">Required parameter: List of user object.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task CreateUsersWithArrayInputAsync(
                List<Models.User> body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/createWithArray");

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
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

            // [200,208] = HTTP OK
            if ((response.StatusCode < 200) || (response.StatusCode > 208))
            {
                throw new ApiException("successful operation", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Creates list of users with given input array.
        /// </summary>
        /// <param name="body">Required parameter: List of user object.</param>
        public void CreateUsersWithListInput(
                List<Models.User> body)
        {
            Task t = this.CreateUsersWithListInputAsync(body);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Creates list of users with given input array.
        /// </summary>
        /// <param name="body">Required parameter: List of user object.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task CreateUsersWithListInputAsync(
                List<Models.User> body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/createWithList");

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
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

            // [200,208] = HTTP OK
            if ((response.StatusCode < 200) || (response.StatusCode > 208))
            {
                throw new ApiException("successful operation", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Get user by user name.
        /// </summary>
        /// <param name="username">Required parameter: The name that needs to be fetched. Use user1 for testing..</param>
        /// <returns>Returns the Models.User response from the API call.</returns>
        public Models.User GetUserByName(
                string username)
        {
            Task<Models.User> t = this.GetUserByNameAsync(username);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Get user by user name.
        /// </summary>
        /// <param name="username">Required parameter: The name that needs to be fetched. Use user1 for testing..</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.User response from the API call.</returns>
        public async Task<Models.User> GetUserByNameAsync(
                string username,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/{username}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "username", username },
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
                throw new ApiException("Invalid username supplied", context);
            }

            if (response.StatusCode == 404)
            {
                throw new ApiException("User not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.User>(response.Body);
        }

        /// <summary>
        /// This can only be done by the logged in user.
        /// </summary>
        /// <param name="username">Required parameter: name that need to be updated.</param>
        /// <param name="body">Required parameter: Updated user object.</param>
        public void UpdateUser(
                string username,
                Models.User body)
        {
            Task t = this.UpdateUserAsync(username, body);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// This can only be done by the logged in user.
        /// </summary>
        /// <param name="username">Required parameter: name that need to be updated.</param>
        /// <param name="body">Required parameter: Updated user object.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task UpdateUserAsync(
                string username,
                Models.User body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/{username}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "username", username },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "Content-Type", "application/json" },
            };

            // append body params.
            var bodyText = ApiHelper.JsonSerialize(body);

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().PutBody(queryBuilder.ToString(), headers, bodyText);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid user supplied", context);
            }

            if (response.StatusCode == 404)
            {
                throw new ApiException("User not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// This can only be done by the logged in user.
        /// </summary>
        /// <param name="username">Required parameter: The name that needs to be deleted.</param>
        public void DeleteUser(
                string username)
        {
            Task t = this.DeleteUserAsync(username);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// This can only be done by the logged in user.
        /// </summary>
        /// <param name="username">Required parameter: The name that needs to be deleted.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task DeleteUserAsync(
                string username,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/{username}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "username", username },
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
                throw new ApiException("Invalid username supplied", context);
            }

            if (response.StatusCode == 404)
            {
                throw new ApiException("User not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Logs user into the system.
        /// </summary>
        /// <param name="username">Required parameter: The user name for login.</param>
        /// <param name="password">Required parameter: The password for login in clear text.</param>
        /// <returns>Returns the string response from the API call.</returns>
        public string LoginUser(
                string username,
                string password)
        {
            Task<string> t = this.LoginUserAsync(username, password);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Logs user into the system.
        /// </summary>
        /// <param name="username">Required parameter: The user name for login.</param>
        /// <param name="password">Required parameter: The password for login in clear text.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        public async Task<string> LoginUserAsync(
                string username,
                string password,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/login");

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "username", username },
                { "password", password },
            };

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers, queryParameters: queryParams);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid username/password supplied", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return response.Body;
        }

        /// <summary>
        /// Logs out current logged in user session.
        /// </summary>
        public void LogoutUser()
        {
            Task t = this.LogoutUserAsync();
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Logs out current logged in user session.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task LogoutUserAsync(CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user/logout");

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

            // [200,208] = HTTP OK
            if ((response.StatusCode < 200) || (response.StatusCode > 208))
            {
                throw new ApiException("successful operation", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// This can only be done by the logged in user.
        /// </summary>
        /// <param name="body">Required parameter: Created user object.</param>
        public void CreateUser(
                Models.User body)
        {
            Task t = this.CreateUserAsync(body);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// This can only be done by the logged in user.
        /// </summary>
        /// <param name="body">Required parameter: Created user object.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task CreateUserAsync(
                Models.User body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/user");

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
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

            // [200,208] = HTTP OK
            if ((response.StatusCode < 200) || (response.StatusCode > 208))
            {
                throw new ApiException("successful operation", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }
    }
}