// <copyright file="IConfiguration.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard
{
    using System;
    using System.Net;
    using SwaggerPetstore.Standard.Authentication;
    using SwaggerPetstore.Standard.Models;

    /// <summary>
    /// IConfiguration.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets Current API environment.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        /// Gets the credentials to use with ImplicitAuth.
        /// </summary>
        IImplicitAuth ImplicitAuth { get; }

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:SERVER1.</param>
        /// <returns>Returns the baseurl.</returns>
        string GetBaseUri(Server alias = Server.Server1);
    }
}