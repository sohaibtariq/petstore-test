// <copyright file="IImplicitAuthCredentials.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Authentication
{
    using System.Collections.Generic;

    public interface IImplicitAuth
    {
        /// <summary>
        /// Gets string value for oAuthClientId.
        /// </summary>
        string OAuthClientId { get; }

        /// <summary>
        /// Gets string value for oAuthRedirectUri.
        /// </summary>
        string OAuthRedirectUri { get; }

        /// <summary>
        /// Gets Models.OAuthToken value for oAuthToken.
        /// </summary>
        Models.OAuthToken OAuthToken { get; }

        /// <summary>
        /// Gets List of Models.OAuthScopeEnum value for oAuthScopes.
        /// </summary>
        List<Models.OAuthScopeEnum> OAuthScopes { get; }

        /// <summary>
        ///  Returns true if credentials matched.
        /// </summary>
        /// <param name="oAuthClientId"> The string value for credentials.</param>
        /// <param name="oAuthRedirectUri"> The string value for credentials.</param>
        /// <param name="oAuthToken"> The Models.OAuthToken value for credentials.</param>
        /// <param name="oAuthScopes"> The List of Models.OAuthScopeEnum value for credentials.</param>
        /// <returns>True if credentials matched.</returns>
        bool Equals(string oAuthClientId, string oAuthRedirectUri, Models.OAuthToken oAuthToken, List<Models.OAuthScopeEnum> oAuthScopes);

        /// <summary>
        /// Checks if token is expired.
        /// </summary>
        /// <returns> Returns true if token is expired.</returns>
        bool IsTokenExpired();

        /// <summary>
        /// Build an authorization URL for taking the user's consent to access data.
        /// </summary>
        /// <param name="state">An opaque state string.</param>
        /// <param name="additionalParameters">Additional parameters to add the the authorization URL.</param>
        /// <returns>Authorization URL.</returns>
        string BuildAuthorizationUrl(string state = null, Dictionary<string, object> additionalParameters = null);
    }
}