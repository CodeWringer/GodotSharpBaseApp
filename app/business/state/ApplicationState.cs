using app.business.model;
using System.Collections.Generic;
using Godot;
using app.business.exception;
using System;
using app.business.dataaccess.applicationsettings;
using System.IO;

namespace app.business.state
{
    /// <summary>
    /// This is the base and global application state object. 
    /// 
    /// It contains all root-level business data. 
    /// </summary>
    public class ApplicationState : ICloneable
    {
        /// <summary>
        /// General application settings. 
        /// </summary>
        public ApplicationSettings Settings { get; private set; }

        /// <summary>
        /// An example of including the model into the application state. Feel free to remove and replace this at will. 
        /// </summary>
        public IEnumerable<AnItem> Items { get; private set; } = new List<AnItem>()
        {
            new AnItem("An Item 1"),
            new AnItem("An Item 2"),
            new AnItem("An Item 3"),
            new AnItem("An Item 4"),
            new AnItem("An Item 5")
        };

        public ApplicationState()
        {
            var repository = new ApplicationSettingsRepository();
            try
            {
                Settings = repository.Read();
            }
            catch (FileNotFoundException)
            {
                Settings = new ApplicationSettings();
                repository.Write(Settings);
            }
        }

        /// <summary>
        /// Returns the ApplicationState from the ApplicationStateNode in the scene tree. 
        /// <br></br>
        /// Requires an instance of a node that is currently in the scene tree to work. 
        /// </summary>
        /// <param name="nodeInSceneTree">Instance of a node that is currently in the scene tree. </param>
        /// <exception cref="MissingNodeException">Thrown, if the ApplicationStateNode could not be fetched. </exception>
        /// <returns>The global application state. </returns>
        public static ApplicationState GetFromSceneTree(Node nodeInSceneTree)
        {
            var node = nodeInSceneTree.GetTree().Root.GetNode<ApplicationStateNode>("ApplicationStateNode");
            if (node == null)
                throw new MissingNodeException();
            else 
                return node.State;
        }

        /// <summary>
        /// Creates a deep copy of this state object. 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Clone()
        {
            return new ApplicationState()
            {
                Items = new List<AnItem>(this.Items),
                Settings = (ApplicationSettings)this.Settings.Clone()
            };
        }

        /// <summary>
        /// Copies the values of the given object to the fields of this object. 
        /// </summary>
        /// <param name="toApply">The object whose values to copy. </param>
        public void Apply(ApplicationState toApply)
        {
            this.Items = toApply.Items;
            this.Settings = toApply.Settings;
        }
    }
}
