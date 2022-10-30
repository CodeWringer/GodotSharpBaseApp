using app.business.state;
using Godot;
using System;

/// <summary>
/// This node holds the global application state. 
/// <br></br>
/// If access to the application state is needed, call ApplicationState.GetFromSceneTree(). 
/// Note, that the associated Godot node is set up as an auto-load with the name "ApplicationStateNode". 
/// </summary>
public class ApplicationStateNode : Control
{
    /// <summary>
    /// Holds the application's root-level business state. This is the actual data to work with. 
    /// </summary>
    public ApplicationState State { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.State = new ApplicationState();
    }
}
