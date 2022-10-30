using app.presentation.validation;
using Godot;
using System;

/// <summary>
/// Represents a LineEdit that 
/// </summary>
public class LineEditAlphanumericOnly : LineEdit
{
    /// <summary>
    /// An instance of a validator that validates string. 
    /// </summary>
    private IValidator<string> _validator;

    /// <summary>
    /// Expected as a child node of this node. 
    /// </summary>
    private InputFeedback _inputFeedbackNode;

    /// <summary>
    /// The last known valid string of this input. This is the text that will be restored when validation 
    /// fails for a newly added text. 
    /// </summary>
    private string _lastValidText;

    private bool _isValid;
    /// <summary>
    /// Is true, if the current value of this input is valid in the current context. Read only.
    /// </summary>
    public bool IsValid
    {
        get { return _isValid; }
        private set { _isValid = value; }
    }

    public override void _Ready()
    {
        _validator = new AlphanumericValidator();
        Connect("text_changed", this, "_on_text_changed");
        _inputFeedbackNode = GetNode<InputFeedback>("InputFeedback");
        _lastValidText = Text ?? string.Empty;
    }

    public void _on_text_changed(string new_text)
    {
        var validation = _validator.Validate(new_text);
        if (validation.Success != true)
        {
            _inputFeedbackNode.Show(validation.Reason.Message);
            _isValid = false;
            Text = _lastValidText;
        }
        else
        {
            _inputFeedbackNode.Hide();
            _isValid = true;
            _lastValidText = new_text;
        }
    }
}

internal class AlphanumericValidator : IValidator<string>
{
    internal static readonly System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

    public ValidationResult Validate(string toValidate)
    {
        var result = regex.Match(toValidate);
        if (result.Success)
            return new ValidationResult(false, new AlphanumericValidationException("Invalid characters detected - only alphanumeric characters allowed!"));
        else
            return new ValidationResult(true, null);
    }
}

internal class AlphanumericValidationException : Exception
{
    public AlphanumericValidationException()
    {
    }

    public AlphanumericValidationException(string message) : base(message)
    {
    }
}