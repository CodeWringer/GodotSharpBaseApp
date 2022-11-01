using System;

namespace app.business.dataaccess
{
    /// <summary>
    /// Represents a writeable data source for data of type T. 
    /// </summary>
    /// <typeparam name="T">The type of data that can be written. </typeparam>
    internal interface IWriteableDataSource<T>
    {
        /// <summary>
        /// Writes the given instance of type T to the data source. 
        /// </summary>
        /// <param name="toWrite">The data to write. </param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown on any error writing the data. </exception>
        void Write(T toWrite);
    }
}
