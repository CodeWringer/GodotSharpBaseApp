using app.business.dataaccess.common.datasource;

namespace app.business.dataaccess.common.repository
{
    /// <summary>
    /// Represents an abstract base class for a readable and writeable repository. 
    /// </summary>
    /// <typeparam name="T">The type of data that can be read/written. </typeparam>
    internal abstract class AbstractReadWriteRepository<T> : IReadableRepository<T>, IWriteableRepository<T>
    {
        /// <summary>
        /// The data source instance used. 
        /// <br></br>
        /// Depending on build parameters or command line arguments, this may be a production or mock version. 
        /// </summary>
        protected AbstractReadWriteDataSource<T> dataSource;

        public virtual T Read()
        {
            return dataSource.Read();
        }

        public virtual void Write(T toWrite)
        {
            dataSource.Write(toWrite);
        }
    }
}
