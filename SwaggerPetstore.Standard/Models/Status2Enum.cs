// <copyright file="Status2Enum.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using SwaggerPetstore.Standard;
    using SwaggerPetstore.Standard.Utilities;

    /// <summary>
    /// Status2Enum.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status2Enum
    {
        /// <summary>
        /// Available.
        /// </summary>
        [EnumMember(Value = "available")]
        Available,

        /// <summary>
        /// Pending.
        /// </summary>
        [EnumMember(Value = "pending")]
        Pending,

        /// <summary>
        /// Sold.
        /// </summary>
        [EnumMember(Value = "sold")]
        Sold
    }
}