using app.business.dataaccess.util;
using System.IO;

namespace app.business.dataaccess.common.datasource
{
    /// <summary>
    /// Represents a file system data source, using Godot's relative paths. 
    /// <br></br>
    /// Writes the data out as json and parses from json. 
    /// </summary>
    /// <typeparam name="T">The type of data to read/write. </typeparam>
    internal class GodotFileSystemDataSource<T> : FileSystemDataSource<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">A Godot relative user dir file path. Must start with the user dir prefix. </param>
        /// <exception cref="DirectoryNotFoundException">Thrown, if the file path does not start with the mandatory 
        /// user dir prefix. See 'PathUtility.USER_DIR_PATH_PREFIX' </exception>
        public GodotFileSystemDataSource(string filePath)
            : base(filePath)
        {
            if (FilePath.StartsWith(PathUtility.USER_DIR_PATH_PREFIX) != true)
                throw new DirectoryNotFoundException("File path lacks required prefix \"" + PathUtility.USER_DIR_PATH_PREFIX + "\"");
        }
    }
}
