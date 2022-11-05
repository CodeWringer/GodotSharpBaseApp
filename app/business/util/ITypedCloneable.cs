namespace app.business.util
{
    /// <summary>
    /// Represents a cloneable object of the given specific data type. 
    /// </summary>
    /// <typeparam name="T">Data type of the object to make cloneable. </typeparam>
    public interface ITypedCloneable<T>
    {
        /// <summary>
        /// Returns a clone of this object. 
        /// </summary>
        /// <returns>A clone of this object. </returns>
        T Clone();
    }
}
