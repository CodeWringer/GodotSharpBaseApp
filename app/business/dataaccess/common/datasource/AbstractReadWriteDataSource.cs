namespace app.business.dataaccess.common.datasource
{
    /// <summary>
    /// Represents an abstract base class for a readable and writeable data source. 
    /// </summary>
    /// <typeparam name="T">The type of data that can be read/written. </typeparam>
    internal abstract class AbstractReadWriteDataSource<T> : IReadableDataSource<T>, IWriteableDataSource<T>
    {
        public abstract T Read();

        public abstract void Write(T toWrite);
    }
}
