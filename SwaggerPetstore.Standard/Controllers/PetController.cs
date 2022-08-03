// <copyright file="PetController.cs" company="APIMatic">
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
    /// PetController.
    /// </summary>
    public class PetController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PetController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        internal PetController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers)
            : base(config, httpClient, authManagers)
        {
        }

        /// <summary>
        /// uploads an image.
        /// </summary>
        /// <param name="petId">Required parameter: ID of pet to update.</param>
        /// <param name="additionalMetadata">Optional parameter: Additional data to pass to server.</param>
        /// <param name="file">Optional parameter: file to upload.</param>
        /// <returns>Returns the Models.ApiResponse response from the API call.</returns>
        public Models.ApiResponse UploadFile(
                long petId,
                string additionalMetadata = null,
                FileStreamInfo file = null)
        {
            Task<Models.ApiResponse> t = this.UploadFileAsync(petId, additionalMetadata, file);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// uploads an image.
        /// </summary>
        /// <param name="petId">Required parameter: ID of pet to update.</param>
        /// <param name="additionalMetadata">Optional parameter: Additional data to pass to server.</param>
        /// <param name="file">Optional parameter: file to upload.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.ApiResponse response from the API call.</returns>
        public async Task<Models.ApiResponse> UploadFileAsync(
                long petId,
                string additionalMetadata = null,
                FileStreamInfo file = null,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet/{petId}/uploadImage");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "petId", petId },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
            };

            var fileHeaders = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "content-type", new[]
                    {
                        string.IsNullOrEmpty(file.ContentType) ? "application/octect-stream" : file.ContentType,
                    }
                },
            };

            // append form/field parameters.
            var fields = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("additionalMetadata", additionalMetadata),
                new KeyValuePair<string, object>("file", CreateFileMultipartContent(file, fileHeaders)),
            };

            // remove null parameters.
            fields = fields.Where(kvp => kvp.Value != null).ToList();

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Post(queryBuilder.ToString(), headers, fields);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.ApiResponse>(response.Body);
        }

        /// <summary>
        /// Add a new pet to the store.
        /// </summary>
        /// <param name="body">Required parameter: Pet object that needs to be added to the store.</param>
        public void AddPet(
                Models.Pet body)
        {
            Task t = this.AddPetAsync(body);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Add a new pet to the store.
        /// </summary>
        /// <param name="body">Required parameter: Pet object that needs to be added to the store.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task AddPetAsync(
                Models.Pet body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet");

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

            if (response.StatusCode == 405)
            {
                throw new ApiException("Invalid input", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Update an existing pet.
        /// </summary>
        /// <param name="body">Required parameter: Pet object that needs to be added to the store.</param>
        public void UpdatePet(
                Models.Pet body)
        {
            Task t = this.UpdatePetAsync(body);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Update an existing pet.
        /// </summary>
        /// <param name="body">Required parameter: Pet object that needs to be added to the store.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task UpdatePetAsync(
                Models.Pet body,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet");

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
                throw new ApiException("Invalid ID supplied", context);
            }

            if (response.StatusCode == 404)
            {
                throw new ApiException("Pet not found", context);
            }

            if (response.StatusCode == 405)
            {
                throw new ApiException("Validation exception", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Multiple status values can be provided with comma separated strings.
        /// </summary>
        /// <param name="status">Required parameter: Status values that need to be considered for filter.</param>
        /// <returns>Returns the List of Models.Pet response from the API call.</returns>
        public List<Models.Pet> FindPetsByStatus(
                List<Models.Status2Enum> status)
        {
            Task<List<Models.Pet>> t = this.FindPetsByStatusAsync(status);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Multiple status values can be provided with comma separated strings.
        /// </summary>
        /// <param name="status">Required parameter: Status values that need to be considered for filter.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the List of Models.Pet response from the API call.</returns>
        public async Task<List<Models.Pet>> FindPetsByStatusAsync(
                List<Models.Status2Enum> status,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet/findByStatus");

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "status", status.Select(a => ApiHelper.JsonSerialize(a).Trim('\"')).ToList() },
            };

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers, queryParameters: queryParams);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid status value", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<List<Models.Pet>>(response.Body);
        }

        /// <summary>
        /// Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.
        /// </summary>
        /// <param name="tags">Required parameter: Tags to filter by.</param>
        /// <returns>Returns the List of Models.Pet response from the API call.</returns>
        [Obsolete]
        public List<Models.Pet> FindPetsByTags(
                List<string> tags)
        {
            Task<List<Models.Pet>> t = this.FindPetsByTagsAsync(tags);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.
        /// </summary>
        /// <param name="tags">Required parameter: Tags to filter by.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the List of Models.Pet response from the API call.</returns>
        [Obsolete]
        public async Task<List<Models.Pet>> FindPetsByTagsAsync(
                List<string> tags,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet/findByTags");

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "tags", tags },
            };

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers, queryParameters: queryParams);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 400)
            {
                throw new ApiException("Invalid tag value", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<List<Models.Pet>>(response.Body);
        }

        /// <summary>
        /// Returns a single pet.
        /// </summary>
        /// <param name="petId">Required parameter: ID of pet to return.</param>
        /// <returns>Returns the Models.Pet response from the API call.</returns>
        public Models.Pet GetPetById(
                long petId)
        {
            Task<Models.Pet> t = this.GetPetByIdAsync(petId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Returns a single pet.
        /// </summary>
        /// <param name="petId">Required parameter: ID of pet to return.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.Pet response from the API call.</returns>
        public async Task<Models.Pet> GetPetByIdAsync(
                long petId,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet/{petId}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "petId", petId },
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
                throw new ApiException("Pet not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<Models.Pet>(response.Body);
        }

        /// <summary>
        /// Updates a pet in the store with form data.
        /// </summary>
        /// <param name="petId">Required parameter: ID of pet that needs to be updated.</param>
        /// <param name="name">Optional parameter: Updated name of the pet.</param>
        /// <param name="status">Optional parameter: Updated status of the pet.</param>
        public void UpdatePetWithForm(
                long petId,
                string name = null,
                string status = null)
        {
            Task t = this.UpdatePetWithFormAsync(petId, name, status);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Updates a pet in the store with form data.
        /// </summary>
        /// <param name="petId">Required parameter: ID of pet that needs to be updated.</param>
        /// <param name="name">Optional parameter: Updated name of the pet.</param>
        /// <param name="status">Optional parameter: Updated status of the pet.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task UpdatePetWithFormAsync(
                long petId,
                string name = null,
                string status = null,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet/{petId}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "petId", petId },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "Content-Type", "application/x-www-form-urlencoded" },
            };

            // append form/field parameters.
            var fields = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("name", name),
                new KeyValuePair<string, object>("status", status),
            };

            // remove null parameters.
            fields = fields.Where(kvp => kvp.Value != null).ToList();

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Post(queryBuilder.ToString(), headers, fields);

            httpRequest = await this.AuthManagers["global"].ApplyAsync(httpRequest).ConfigureAwait(false);

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);

            if (response.StatusCode == 405)
            {
                throw new ApiException("Invalid input", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }

        /// <summary>
        /// Deletes a pet.
        /// </summary>
        /// <param name="petId">Required parameter: Pet id to delete.</param>
        /// <param name="apiKey">Optional parameter: Example: .</param>
        public void DeletePet(
                long petId,
                string apiKey = null)
        {
            Task t = this.DeletePetAsync(petId, apiKey);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Deletes a pet.
        /// </summary>
        /// <param name="petId">Required parameter: Pet id to delete.</param>
        /// <param name="apiKey">Optional parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task DeletePetAsync(
                long petId,
                string apiKey = null,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/pet/{petId}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "petId", petId },
            });

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "api_key", apiKey },
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
                throw new ApiException("Pet not found", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);
        }
    }
}