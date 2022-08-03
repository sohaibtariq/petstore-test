// <copyright file="ImplicitAuthManager.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Text;
    using SwaggerPetstore.Standard.Http.Request;
    using SwaggerPetstore.Standard.Utilities;
    using SwaggerPetstore.Standard.Models;
    using Dropbox.Standard.Exceptions;

    /// <summary>
    /// ImplicitAuthManager Class.
    /// </summary>
    public class ImplicitAuthManager : IImplicitAuth, IAuthManager
    {
        private readonly IConfiguration Config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplicitAuthManager"/> class.
        /// </summary>
        /// <param name="oAuthClientId"> OAuth 2 Client ID.</param>
        /// <param name="oAuthRedirectUri"> OAuth 2 Redirection URI.</param>
        /// <param name="oAuthToken"> OAuth 2 token.</param>
        /// <param name="oAuthScopes"> List of OAuth 2 scopes.</param>
        /// <param name="config"> Instance of IConfiguration.</param>
        public ImplicitAuthManager(
            string oAuthClientId,
            string oAuthRedirectUri,
            Models.OAuthToken oAuthToken,
            List<Models.OAuthScopeEnum> oAuthScopes,
            IConfiguration config)
        {
            this.OAuthClientId = oAuthClientId;
            this.OAuthRedirectUri = oAuthRedirectUri;
            this.OAuthToken = oAuthToken;
            this.OAuthScopes = oAuthScopes;
            this.Config = config;
        }

        /// <summary>
        /// Gets string value for oAuthClientId.
        /// </summary>
        public string OAuthClientId { get; }

        /// <summary>
        /// Gets string value for oAuthRedirectUri.
        /// </summary>
        public string OAuthRedirectUri { get; }

        /// <summary>
        /// Gets Models.OAuthToken value for oAuthToken.
        /// </summary>
        public Models.OAuthToken OAuthToken { get; }

        /// <summary>
        /// Gets List of Models.OAuthScopeEnum value for oAuthScopes.
        /// </summary>
        public List<Models.OAuthScopeEnum> OAuthScopes { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="oAuthClientId"> The string value for credentials.</param>
        /// <param name="oAuthRedirectUri"> The string value for credentials.</param>
        /// <param name="oAuthToken"> The Models.OAuthToken value for credentials.</param>
        /// <param name="oAuthScopes"> The List of Models.OAuthScopeEnum value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string oAuthClientId, string oAuthRedirectUri, Models.OAuthToken oAuthToken, List<Models.OAuthScopeEnum> oAuthScopes)
        {
            return oAuthClientId.Equals(this.OAuthClientId)
                    && oAuthRedirectUri.Equals(this.OAuthRedirectUri)
                    && ((oAuthToken == null && this.OAuthToken == null) || (oAuthToken != null && this.OAuthToken != null && oAuthToken.Equals(this.OAuthToken)))
                    && ((oAuthScopes == null && this.OAuthScopes == null) || (oAuthScopes != null && this.OAuthScopes != null && oAuthScopes.Equals(this.OAuthScopes)));
        }

        /// <summary>
        /// Checks if token is expired.
        /// </summary>
        /// <returns> Returns true if token is expired.</returns>
        public bool IsTokenExpired()
        {
           if (this.OAuthToken == null)
           {
               throw new InvalidOperationException("OAuth token is missing.");
           }
        
           return this.OAuthToken.Expiry != null
               && this.OAuthToken.Expiry < (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        
        /// <summary>
        /// Add authentication information to the HTTP Request.
        /// </summary>
        /// <param name="httpRequest">The http request object on which authentication will be applied.</param>
        /// <exception cref="ApiException">Thrown when OAuthToken is null or expired.</exception>
        /// <returns>HttpRequest.</returns>
        public HttpRequest Apply(HttpRequest httpRequest)
        {
            Task<HttpRequest> t = this.ApplyAsync(httpRequest);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Asynchronously add authentication information to the HTTP Request.
        /// </summary>
        /// <param name="httpRequest">The http request object on which authentication will be applied.</param>
        /// <exception cref="ApiException">Thrown when OAuthToken is null or expired.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<HttpRequest> ApplyAsync(HttpRequest httpRequest)
        {
            return Task<HttpRequest>.Factory.StartNew(() =>
            {
                CheckAuthorization();
                httpRequest.Headers["Authorization"] = $"Bearer {this.OAuthToken.AccessToken}";
                return httpRequest;
            });
        }

        /// <summary>
        /// Builds the authorization url for making authorized calls.
        /// </summary>
        /// <param name="state">State</param>        
        /// <param name="additionalParameters">Additional parameters to be appended</param>        
        public string BuildAuthorizationUrl(string state = null, Dictionary<string, object> additionalParameters = null)
        {
            // the base uri for api requests
            string baseUri = this.Config.GetBaseUri(Server.AuthServer);

            // prepare query string for API call
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/authorize");

            ApiHelper.AppendUrlWithQueryParameters(queryBuilder, new Dictionary<string, object>
            {
                { "response_type", "token" },
                { "client_id", this.OAuthClientId},
                { "redirect_uri", this.OAuthRedirectUri},
                { "scope", this.OAuthScopes.GetValues() },
                { "state", state}
            });

            if (additionalParameters != null)
                ApiHelper.AppendUrlWithQueryParameters(queryBuilder, additionalParameters);

            // validate and preprocess url
            string queryUrl = ApiHelper.CleanUrl(queryBuilder);

            return queryUrl;
        }

        /// <summary>
        /// Checks if client is authorized.
        /// </summary>
        private void CheckAuthorization()
        {
            if (this.OAuthToken == null)
            {
                throw new ApiException(
                        "Client is not authorized. An OAuth token is needed to make API calls.");
            }

            if (IsTokenExpired())
            {
                throw new ApiException(
                        "OAuth token is expired. A valid token is needed to make API calls.");
            }
        }
    }
}