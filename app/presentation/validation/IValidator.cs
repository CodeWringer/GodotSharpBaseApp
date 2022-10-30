namespace app.presentation.validation
{
    /// <summary>
    /// Validates data of the given type regarding the validation rules of the concrete implementation of this type. 
    /// </summary>
    /// <typeparam name="T">The type of data that can be validated. </typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validates the given input value. 
        /// </summary>
        /// <param name="toValidate">The value to validate. </param>
        /// <returns>A ValidationResult indicating whether validation was successful and if not, provides the reason wny. </returns>
        ValidationResult Validate(T toValidate);
    }
}
