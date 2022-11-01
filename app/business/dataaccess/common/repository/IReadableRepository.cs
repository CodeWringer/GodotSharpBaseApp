using System;

namespace app.business.dataaccess
{
    /// <summary>
    /// Represents a readable repository for data of type T. 
    /// <br></br>
    /// It is the repository's job to choose the correct data source (e.g. local or remote, production or mock, etc.) and  
    /// to handle caching (if there is caching). 
    /// </summary>
    /// <typeparam name="T">The type of data that can be loaded. </typeparam>
    internal interface IReadableRepository<T>
    {
        /// <summary>
        /// Returns an instance of the data from the repository. 
        /// </summary>
        /// <returns>An instance fetched from the repository. </returns>
        /// <exception cref="Exception">Thrown on any error fetching the data. </exception>
        T Read();
    }
}
