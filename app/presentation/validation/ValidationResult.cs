using System;

namespace app.presentation.validation
{
    /// <summary>
    /// Represents the result of a value validation. 
    /// <br></br>
    /// Holds a value indicating whether validation was successful and if not, provides the reason wny. 
    /// </summary>
    public struct ValidationResult
    {
        /// <summary>
        /// If true, the validation was successful, meaning the value tested is valid for use. 
        /// <br></br>
        /// If false, see the corresponding Reason property. 
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// The reason why validation failed. Is null, in case validation succeeded. 
        /// </summary>
        public Exception Reason { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success">Whether validation was a success. 
        /// If true, a reason must <b>not</b> be provided. 
        /// If false, a reason <b>must</b> be provided </param>
        /// <param name="reason">The reason why validation failed, or null, if validation was successful. </param>
        /// <exception cref="ArgumentNullException">Thrown, if the given success and reason arguments don't match. </exception>
        public ValidationResult(bool success, Exception reason)
        {
            if (!success && reason == null)
                throw new ArgumentNullException(nameof(reason), "When success is false, a reason why must be provided!");
            else if (success && reason != null)
                throw new ArgumentNullException(nameof(reason), "When success is true, a reason why must not be provided!");

            Success = success;
            Reason = reason;
        }
    }
}
