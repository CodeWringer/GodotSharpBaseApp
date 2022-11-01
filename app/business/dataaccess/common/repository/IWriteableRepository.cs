using System;

namespace app.business.dataaccess
{
    /// <summary>
    /// Represents a writeable repository for data of type T. 
    /// <br></br>
    /// It is the repository's job to choose the correct data source (e.g. local or remote, production or mock, etc.) and  
    /// to handle caching (if there is caching). 
    /// </summary>
    /// <typeparam name="T">The type of data that can be loaded. </typeparam>
    internal interface IWriteableRepository<T>
    {
        /// <summary>
        /// Writes the given instance of type T to the repository. 
        /// </summary>
        /// <param name="toWrite">The data to write. </param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown on any error writing the data. </exception>
        void Write(T toWrite);
    }
}
