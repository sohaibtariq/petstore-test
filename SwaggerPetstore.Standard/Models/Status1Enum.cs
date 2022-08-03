// <copyright file="Status1Enum.cs" company="APIMatic">
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
    /// Status1Enum.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status1Enum
    {
        /// <summary>
        /// Placed.
        /// </summary>
        [EnumMember(Value = "placed")]
        Placed,

        /// <summary>
        /// Approved.
        /// </summary>
        [EnumMember(Value = "approved")]
        Approved,

        /// <summary>
        /// Delivered.
        /// </summary>
        [EnumMember(Value = "delivered")]
        Delivered
    }
}