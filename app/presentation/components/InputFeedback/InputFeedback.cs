using Godot;
using System;

/// <summary>
/// Represents an overlay over inputs to show a user if their input isn't valid in the given context. 
/// <br></br>
/// This Godot node is intended to be added as a child node of an input node. 
/// </summary>
public class InputFeedback : Panel
{
    private PopupPanel _popupError;

    private Label _labelError;

    private Control _parent;

    private bool _isShowingError;
    /// <summary>
    /// Is true, if the error message is currently shown. Readonly. 
    /// </summary>
    public bool IsShowingError
    {
        get => _isShowingError;
        private set => _isShowingError = value;
    }

    public override void _Ready()
    {
        _popupError = GetNode<PopupPanel>("PopupError");
        _labelError = GetNode<Label>("PopupError/LabelError");
        _parent = (Control)GetParent();

        Visible = false;
    }

    /// <summary>
    /// Displays an error outline, but no message pop up. 
    /// </summary>
    public new void Show()
    {
        _labelError.Text = string.Empty;
        _isShowingError = true;

        Visible = true;
        _popupError.Hide();

        base.Show();
    }

    /// <summary>
    /// Displays the given error message. 
    /// </summary>
    /// <param name="message">A localized text explaining the reason why input was invalid. </param>
    public void Show(string message)
    {
        _labelError.Text = message;
        _isShowingError = true;

        Visible = true;
        var rect = new Rect2(
            _parent.RectGlobalPosition.x,
            _parent.RectGlobalPosition.y + _parent.RectSize.y,
            _parent.RectSize
        );
        _popupError.Popup_(rect);

        base.Show();
    }

    /// <summary>
    /// Hides the error message, if it is currently shown. 
    /// </summary>
    public new void Hide()
    {
        _isShowingError = false;
        _popupError.Hide();
        Visible = false;

        base.Hide();
    }
}
