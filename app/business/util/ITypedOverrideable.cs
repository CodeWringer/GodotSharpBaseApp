namespace app.business.util
{
    /// <summary>
    /// Represents a type whose field's values can be easily overwritten with the values of another instance of 
    /// the same type. 
    /// </summary>
    /// <typeparam name="T">The type of object whose fields are to be overrideable. </typeparam>
    public interface ITypedOverrideable<T>
    {
        /// <summary>
        /// Copies the values of the given object's fields and applies them to this object's corresponding fields. 
        /// </summary>
        /// <param name="toApply">The other object whose values to apply/copy. </param>
        void Apply(T toApply);
    }
}
