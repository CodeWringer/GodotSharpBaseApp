using System;
using System.Collections.Generic;

namespace app.business.dataaccess
{
    /// <summary>
    /// Represents a readable data source for data of type T. 
    /// </summary>
    /// <typeparam name="T">The type of data that can be loaded. </typeparam>
    internal interface IReadableDataSource<T>
    {
        /// <summary>
        /// Returns an instance of the data from the data source. 
        /// </summary>
        /// <returns>An instance fetched from the data source. </returns>
        /// <exception cref="Exception">Thrown on any error fetching the data. </exception>
        T Read();
    }
}
