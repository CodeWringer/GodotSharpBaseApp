using System;

namespace app.business.state
{
    /// <summary>
    /// Represents general user-made settings of the app that are/can be persisted between runs of the app. 
    /// </summary>
    public class ApplicationSettings : ICloneable
    {
        /// <summary>
        /// The Godot-specific uri of the application settings. 
        /// </summary>
        public static readonly string PATH_APP_SETTINGS = "user://app_settings.json";

        public object Clone()
        {
            return new ApplicationSettings();
        }
    }
}
