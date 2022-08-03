// <copyright file="OAuthScopeEnum.cs" company="APIMatic">
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
    using System.Reflection;

    /// <summary>
    /// OAuthScopeEnum.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OAuthScopeEnum
    {
        /// <summary>
        ///read your pets
        /// Readpets.
        /// </summary>
        [EnumMember(Value = "read:pets")]
        Readpets,

        /// <summary>
        ///modify pets in your account
        /// Writepets.
        /// </summary>
        [EnumMember(Value = "write:pets")]
        Writepets
    }

    static class OAuthScopeEnumExtention
    {
        internal static string GetValues(this IEnumerable<OAuthScopeEnum> values)
        {
            return values != null ? string.Join(" ", values.Select(s => s.GetValue()).Where(s => !string.IsNullOrEmpty(s)).ToArray()) : null;
        }

        private static string GetValue(this Enum value)
        {
            return value.GetType()
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }
}