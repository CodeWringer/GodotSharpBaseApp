using app.business.state;
using Godot;

/// <summary>
/// The entry point to the application. 
/// </summary>
public class Main : Control
{
    /// <summary>
    /// A reference to the state object, as fetched from the auto-loaded ApplicationStateNode. 
    /// </summary>
    internal ApplicationState State { get; private set; }

    public override void _Ready()
    {
        State = ApplicationState.GetFromSceneTree(this);
    }
}
