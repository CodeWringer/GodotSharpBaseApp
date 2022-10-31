using System.IO;
using Utf8Json;

namespace app.business.dataaccess
{
    /// <summary>
    /// Represents a file system data source, using Godot's relative paths. 
    /// <br></br>
    /// Writes the data out as json and parses from json. 
    /// </summary>
    /// <typeparam name="T">The type of data to read/write. </typeparam>
    internal abstract class GodotFilesystemDataSource<T> : IReadableDataSource<T>, IWriteableDataSource<T>
    {
        /// <summary>
        /// Returns the absolute file path, using Godot's relative paths. 
        /// <br></br>
        /// Must start either with "user://". If it doesn't, attempting to read or write using this file path, 
        /// if it is lacking the "user://"-prefix, will result in an exception. 
        /// </summary>
        public abstract string FilePath { get; protected set; }

        /// <summary>
        /// Godot specific path prefix for user-specific files. 
        /// </summary>
        public static readonly string USER_PREFIX = "user://";

        /// <summary>
        /// Reads data from the file system, using Godot's relative paths and returns an instance of type T parsed from the file contents. 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException">Thrown if the path doesn't point to the user-directory. </exception>
        /// <exception cref="FileNotFoundException">Thrown, if the file does not exist. </exception>
        public T Read()
        {
            if (FilePath.StartsWith(USER_PREFIX) != true)
                throw new DirectoryNotFoundException("File path lacks required prefix \"" + USER_PREFIX + "\"");

            var file = new Godot.File();

            if (file.FileExists(this.FilePath) != true)
                throw new FileNotFoundException();

            file.Open(this.FilePath, Godot.File.ModeFlags.Read);
            string content = file.GetAsText();
            file.Close();
            return JsonSerializer.Deserialize<T>(content);
        }

        /// <summary>
        /// Writes the given instance of type T to the user directory. 
        /// </summary>
        /// <param name="toWrite">The instance to write out. </param>
        /// <exception cref="DirectoryNotFoundException">Thrown if the path doesn't point to the user-directory. </exception>
        public void Write(T toWrite)
        {
            if (FilePath.StartsWith(USER_PREFIX) != true)
                throw new DirectoryNotFoundException("File path lacks required prefix \"" + USER_PREFIX + "\"");

            var file = new Godot.File();
            file.Open(this.FilePath, Godot.File.ModeFlags.Write);
            string content = JsonSerializer.ToJsonString(toWrite);
            file.StoreString(content);
            file.Close();
        }
    }
}
