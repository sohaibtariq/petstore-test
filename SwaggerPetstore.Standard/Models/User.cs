// <copyright file="User.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using SwaggerPetstore.Standard;
    using SwaggerPetstore.Standard.Utilities;

    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="username">username.</param>
        /// <param name="firstName">firstName.</param>
        /// <param name="lastName">lastName.</param>
        /// <param name="email">email.</param>
        /// <param name="password">password.</param>
        /// <param name="phone">phone.</param>
        /// <param name="userStatus">userStatus.</param>
        public User(
            long? id = null,
            string username = null,
            string firstName = null,
            string lastName = null,
            string email = null,
            string password = null,
            string phone = null,
            int? userStatus = null)
        {
            this.Id = id;
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Phone = phone;
            this.UserStatus = userStatus;
        }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets Username.
        /// </summary>
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets FirstName.
        /// </summary>
        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets Phone.
        /// </summary>
        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        /// <summary>
        /// User Status
        /// </summary>
        [JsonProperty("userStatus", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserStatus { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var toStringOutput = new List<string>();

            this.ToString(toStringOutput);

            return $"User : ({string.Join(", ", toStringOutput)})";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }

            return obj is User other &&
                ((this.Id == null && other.Id == null) || (this.Id?.Equals(other.Id) == true)) &&
                ((this.Username == null && other.Username == null) || (this.Username?.Equals(other.Username) == true)) &&
                ((this.FirstName == null && other.FirstName == null) || (this.FirstName?.Equals(other.FirstName) == true)) &&
                ((this.LastName == null && other.LastName == null) || (this.LastName?.Equals(other.LastName) == true)) &&
                ((this.Email == null && other.Email == null) || (this.Email?.Equals(other.Email) == true)) &&
                ((this.Password == null && other.Password == null) || (this.Password?.Equals(other.Password) == true)) &&
                ((this.Phone == null && other.Phone == null) || (this.Phone?.Equals(other.Phone) == true)) &&
                ((this.UserStatus == null && other.UserStatus == null) || (this.UserStatus?.Equals(other.UserStatus) == true));
        }
        

        /// <summary>
        /// ToString overload.
        /// </summary>
        /// <param name="toStringOutput">List of strings.</param>
        protected void ToString(List<string> toStringOutput)
        {
            toStringOutput.Add($"this.Id = {(this.Id == null ? "null" : this.Id.ToString())}");
            toStringOutput.Add($"this.Username = {(this.Username == null ? "null" : this.Username == string.Empty ? "" : this.Username)}");
            toStringOutput.Add($"this.FirstName = {(this.FirstName == null ? "null" : this.FirstName == string.Empty ? "" : this.FirstName)}");
            toStringOutput.Add($"this.LastName = {(this.LastName == null ? "null" : this.LastName == string.Empty ? "" : this.LastName)}");
            toStringOutput.Add($"this.Email = {(this.Email == null ? "null" : this.Email == string.Empty ? "" : this.Email)}");
            toStringOutput.Add($"this.Password = {(this.Password == null ? "null" : this.Password == string.Empty ? "" : this.Password)}");
            toStringOutput.Add($"this.Phone = {(this.Phone == null ? "null" : this.Phone == string.Empty ? "" : this.Phone)}");
            toStringOutput.Add($"this.UserStatus = {(this.UserStatus == null ? "null" : this.UserStatus.ToString())}");
        }
    }
}