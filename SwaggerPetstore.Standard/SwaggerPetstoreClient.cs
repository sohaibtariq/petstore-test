// <copyright file="SwaggerPetstoreClient.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using SwaggerPetstore.Standard.Authentication;
    using SwaggerPetstore.Standard.Controllers;
    using SwaggerPetstore.Standard.Http.Client;
    using SwaggerPetstore.Standard.Utilities;

    /// <summary>
    /// The gateway for the SDK. This class acts as a factory for Controller and
    /// holds the configuration of the SDK.
    /// </summary>
    public sealed class SwaggerPetstoreClient : IConfiguration
    {
        // A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environment, Dictionary<Server, string>> EnvironmentsMap =
            new Dictionary<Environment, Dictionary<Server, string>>
        {
            {
                Environment.Production, new Dictionary<Server, string>
                {
                    { Server.Server1, "https://petstore.swagger.io/v2" },
                    { Server.Server2, "http://petstore.swagger.io/v2" },
                    { Server.AuthServer, "https://petstore.swagger.io/oauth" },
                }
            },
        };

        private readonly IDictionary<string, IAuthManager> authManagers;
        private readonly IHttpClient httpClient;
        private readonly ImplicitAuthManager implicitAuthManager;

        private readonly Lazy<PetController> pet;
        private readonly Lazy<StoreController> store;
        private readonly Lazy<UserController> user;

        private SwaggerPetstoreClient(
            Environment environment,
            string oAuthClientId,
            string oAuthRedirectUri,
            Models.OAuthToken oAuthToken,
            List<Models.OAuthScopeEnum> oAuthScopes,
            IDictionary<string, IAuthManager> authManagers,
            IHttpClient httpClient,
            IHttpClientConfiguration httpClientConfiguration)
        {
            this.Environment = environment;
            this.httpClient = httpClient;
            this.authManagers = (authManagers == null) ? new Dictionary<string, IAuthManager>() : new Dictionary<string, IAuthManager>(authManagers);
            this.HttpClientConfiguration = httpClientConfiguration;

            this.pet = new Lazy<PetController>(
                () => new PetController(this, this.httpClient, this.authManagers));
            this.store = new Lazy<StoreController>(
                () => new StoreController(this, this.httpClient, this.authManagers));
            this.user = new Lazy<UserController>(
                () => new UserController(this, this.httpClient, this.authManagers));

            if (this.authManagers.ContainsKey("global"))
            {
                this.implicitAuthManager = (ImplicitAuthManager)this.authManagers["global"];
            }

            if (!this.authManagers.ContainsKey("global")
                || !this.ImplicitAuth.Equals(oAuthClientId, oAuthRedirectUri, oAuthToken, oAuthScopes))
            {
                this.implicitAuthManager = new ImplicitAuthManager(oAuthClientId, oAuthRedirectUri, oAuthToken, oAuthScopes, this);
                this.authManagers["global"] = this.implicitAuthManager;
            }
        }

        /// <summary>
        /// Gets PetController controller.
        /// </summary>
        public PetController PetController => this.pet.Value;

        /// <summary>
        /// Gets StoreController controller.
        /// </summary>
        public StoreController StoreController => this.store.Value;

        /// <summary>
        /// Gets UserController controller.
        /// </summary>
        public UserController UserController => this.user.Value;

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
        /// Gets Environment.
        /// Current API environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets auth managers.
        /// </summary>
        internal IDictionary<string, IAuthManager> AuthManagers => this.authManagers;

        /// <summary>
        /// Gets http client.
        /// </summary>
        internal IHttpClient HttpClient => this.httpClient;

        /// <summary>
        /// Gets the credentials to use with ImplicitAuth.
        /// </summary>
        public IImplicitAuth ImplicitAuth => this.implicitAuthManager;

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends
        /// it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:SERVER1.</param>
        /// <returns>Returns the baseurl.</returns>
        public string GetBaseUri(Server alias = Server.Server1)
        {
            StringBuilder url = new StringBuilder(EnvironmentsMap[this.Environment][alias]);
            ApiHelper.AppendUrlWithTemplateParameters(url, this.GetBaseUriParameters());

            return url.ToString();
        }

        /// <summary>
        /// Creates an object of the SwaggerPetstoreClient using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            Builder builder = new Builder()
                .Environment(this.Environment)
                .OAuthToken(this.implicitAuthManager.OAuthToken)
                .OAuthScopes(this.implicitAuthManager.OAuthScopes)
                .ImplicitAuth(this.implicitAuthManager.OAuthClientId, this.implicitAuthManager.OAuthRedirectUri)
                .HttpClient(this.httpClient)
                .AuthManagers(this.authManagers)
                .HttpClientConfig(config => config.Build());

            return builder;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return
                $"Environment = {this.Environment}, " +
                $"HttpClientConfiguration = {this.HttpClientConfiguration}, ";
        }

        /// <summary>
        /// Creates the client using builder.
        /// </summary>
        /// <returns> SwaggerPetstoreClient.</returns>
        internal static SwaggerPetstoreClient CreateFromEnvironment()
        {
            var builder = new Builder();

            string environment = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_ENVIRONMENT");
            string oAuthClientId = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_O_AUTH_CLIENT_ID");
            string oAuthRedirectUri = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_O_AUTH_REDIRECT_URI");

            if (environment != null)
            {
                builder.Environment(ApiHelper.JsonDeserialize<Environment>($"\"{environment}\""));
            }

            if (oAuthClientId != null && oAuthRedirectUri != null)
            {
                builder.ImplicitAuth(oAuthClientId, oAuthRedirectUri);
            }

            return builder.Build();
        }

        /// <summary>
        /// Makes a list of the BaseURL parameters.
        /// </summary>
        /// <returns>Returns the parameters list.</returns>
        private List<KeyValuePair<string, object>> GetBaseUriParameters()
        {
            List<KeyValuePair<string, object>> kvpList = new List<KeyValuePair<string, object>>()
            {
            };
            return kvpList;
        }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private Environment environment = SwaggerPetstore.Standard.Environment.Production;
            private string oAuthClientId = "";
            private string oAuthRedirectUri = "";
            private Models.OAuthToken oAuthToken = null;
            private List<Models.OAuthScopeEnum> oAuthScopes = null;
            private IDictionary<string, IAuthManager> authManagers = new Dictionary<string, IAuthManager>();
            private HttpClientConfiguration.Builder httpClientConfig = new HttpClientConfiguration.Builder();
            private IHttpClient httpClient;

            /// <summary>
            /// Sets credentials for ImplicitAuth.
            /// </summary>
            /// <param name="oAuthClientId">OAuthClientId.</param>
            /// <param name="oAuthRedirectUri">OAuthRedirectUri.</param>
            /// <returns>Builder.</returns>
            public Builder ImplicitAuth(string oAuthClientId, string oAuthRedirectUri)
            {
                this.oAuthClientId = oAuthClientId ?? throw new ArgumentNullException(nameof(oAuthClientId));
                this.oAuthRedirectUri = oAuthRedirectUri ?? throw new ArgumentNullException(nameof(oAuthRedirectUri));
                return this;
            }

            /// <summary>
            /// Sets OAuthToken.
            /// </summary>
            /// <param name="oAuthToken">OAuthToken.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthToken(Models.OAuthToken oAuthToken)
            {
                this.oAuthToken = oAuthToken;
                return this;
            }

            /// <summary>
            /// Sets OAuthScopes.
            /// </summary>
            /// <param name="oAuthScopes">OAuthScopes.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthScopes(List<Models.OAuthScopeEnum> oAuthScopes)
            {
                this.oAuthScopes = oAuthScopes;
                return this;
            }

            /// <summary>
            /// Sets Environment.
            /// </summary>
            /// <param name="environment"> Environment. </param>
            /// <returns> Builder. </returns>
            public Builder Environment(Environment environment)
            {
                this.environment = environment;
                return this;
            }

            /// <summary>
            /// Sets HttpClientConfig.
            /// </summary>
            /// <param name="action"> Action. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientConfig(Action<HttpClientConfiguration.Builder> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                action(this.httpClientConfig);
                return this;
            }

            /// <summary>
            /// Sets the IHttpClient for the Builder.
            /// </summary>
            /// <param name="httpClient"> http client. </param>
            /// <returns>Builder.</returns>
            internal Builder HttpClient(IHttpClient httpClient)
            {
                this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
                return this;
            }

            /// <summary>
            /// Sets the authentication managers for the Builder.
            /// </summary>
            /// <param name="authManagers"> auth managers. </param>
            /// <returns>Builder.</returns>
            internal Builder AuthManagers(IDictionary<string, IAuthManager> authManagers)
            {
                this.authManagers = authManagers ?? throw new ArgumentNullException(nameof(authManagers));
                return this;
            }

            /// <summary>
            /// Creates an object of the SwaggerPetstoreClient using the values provided for the builder.
            /// </summary>
            /// <returns>SwaggerPetstoreClient.</returns>
            public SwaggerPetstoreClient Build()
            {
                this.httpClient = new HttpClientWrapper(this.httpClientConfig.Build());

                return new SwaggerPetstoreClient(
                    this.environment,
                    this.oAuthClientId,
                    this.oAuthRedirectUri,
                    this.oAuthToken,
                    this.oAuthScopes,
                    this.authManagers,
                    this.httpClient,
                    this.httpClientConfig.Build());
            }
        }
    }
}
