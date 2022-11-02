using System;
using System.Text;
using System.Text.RegularExpressions;

namespace app.business.dataaccess.util
{
    /// <summary>
    /// Provides utility functions for working with and validating file/directory paths. 
    /// </summary>
    public static class PathUtility
    {
        /// <summary>
        /// Regex to detect the validity of a file path's format. 
        /// </summary>
        private static Regex rgxFilePath = new Regex("^(((user|ref)://)|([a-zA-Z]:[/\\\\])|~/?)([\\w\\s]+/)*([\\w\\s]+(\\.[\\w\\s]+)?)$");

        /// <summary>
        /// Godot specific path prefix for user-specific files. The exact location in the file system this resolves to, depends on 
        /// the project and whether a custom dir has been configured. If no custom dir is configured, it resolves as follows. 
        /// <br></br>
        /// Windows: %APPDATA%/Godot/app_userdata/[project_name]
        /// <br></br>
        /// macOS: ~/Library/Application Support/Godot/app_userdata/[project_name]
        /// <br></br>
        /// Linux: ~/.local/share/godot/app_userdata/[project_name]
        /// </summary>
        public static readonly string USER_DIR_PATH_PREFIX = "user://";

        /// <summary>
        /// Godot specific path prefix for project-specific files. The exact location in the file system resolves to the directory 
        /// that contians the 'project.godot' file. 
        /// </summary>
        public static readonly string RESOURCE_DIR_PATH_PREFIX = "res://";

        /// <summary>
        /// Returns true, if the given file path is in a valid format. 
        /// </summary>
        /// <param name="filePath">The file path to test. </param>
        /// <returns>True, if the file path is in valid format. </returns>
        public static bool IsFilePathValid(string filePath)
        {
            return rgxFilePath.Match(filePath).Success;
        }
    }
}
