using app.business.util;

namespace app.business.state
{
    /// <summary>
    /// Represents the basis for an application state object, which contains all the application's business state. 
    /// </summary>
    /// <typeparam name="T">The data type of the implementing data type. E. g. "ApplicationState". </typeparam>
    public abstract class AbstractApplicationState<T> : ITypedCloneable<T>, ITypedOverrideable<T>
    {
        public abstract void Apply(T toApply);

        public abstract T Clone();
    }
}
