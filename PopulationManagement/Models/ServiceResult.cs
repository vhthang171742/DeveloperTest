// <copyright file="ServiceResult.cs" company="Placeholder Company">
// Copyright (c) Placeholder Company. All rights reserved.
// </copyright>

namespace Models
{
    /// <summary>
    /// Class that holds result of a service call.
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether success state.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets response message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets returned data (if needed).
        /// </summary>
        public object Data { get; set; }
    }
}
