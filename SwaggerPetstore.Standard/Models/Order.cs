// <copyright file="Order.cs" company="APIMatic">
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
    /// Order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="petId">petId.</param>
        /// <param name="quantity">quantity.</param>
        /// <param name="shipDate">shipDate.</param>
        /// <param name="status">status.</param>
        /// <param name="complete">complete.</param>
        public Order(
            long? id = null,
            long? petId = null,
            int? quantity = null,
            DateTime? shipDate = null,
            Models.Status1Enum? status = null,
            bool? complete = null)
        {
            this.Id = id;
            this.PetId = petId;
            this.Quantity = quantity;
            this.ShipDate = shipDate;
            this.Status = status;
            this.Complete = complete;
        }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets PetId.
        /// </summary>
        [JsonProperty("petId", NullValueHandling = NullValueHandling.Ignore)]
        public long? PetId { get; set; }

        /// <summary>
        /// Gets or sets Quantity.
        /// </summary>
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets ShipDate.
        /// </summary>
        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("shipDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        [JsonProperty("status", ItemConverterType = typeof(StringEnumConverter), NullValueHandling = NullValueHandling.Ignore)]
        public Models.Status1Enum? Status { get; set; }

        /// <summary>
        /// Gets or sets Complete.
        /// </summary>
        [JsonProperty("complete", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Complete { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var toStringOutput = new List<string>();

            this.ToString(toStringOutput);

            return $"Order : ({string.Join(", ", toStringOutput)})";
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

            return obj is Order other &&
                ((this.Id == null && other.Id == null) || (this.Id?.Equals(other.Id) == true)) &&
                ((this.PetId == null && other.PetId == null) || (this.PetId?.Equals(other.PetId) == true)) &&
                ((this.Quantity == null && other.Quantity == null) || (this.Quantity?.Equals(other.Quantity) == true)) &&
                ((this.ShipDate == null && other.ShipDate == null) || (this.ShipDate?.Equals(other.ShipDate) == true)) &&
                ((this.Status == null && other.Status == null) || (this.Status?.Equals(other.Status) == true)) &&
                ((this.Complete == null && other.Complete == null) || (this.Complete?.Equals(other.Complete) == true));
        }
        

        /// <summary>
        /// ToString overload.
        /// </summary>
        /// <param name="toStringOutput">List of strings.</param>
        protected void ToString(List<string> toStringOutput)
        {
            toStringOutput.Add($"this.Id = {(this.Id == null ? "null" : this.Id.ToString())}");
            toStringOutput.Add($"this.PetId = {(this.PetId == null ? "null" : this.PetId.ToString())}");
            toStringOutput.Add($"this.Quantity = {(this.Quantity == null ? "null" : this.Quantity.ToString())}");
            toStringOutput.Add($"this.ShipDate = {(this.ShipDate == null ? "null" : this.ShipDate.ToString())}");
            toStringOutput.Add($"this.Status = {(this.Status == null ? "null" : this.Status.ToString())}");
            toStringOutput.Add($"this.Complete = {(this.Complete == null ? "null" : this.Complete.ToString())}");
        }
    }
}