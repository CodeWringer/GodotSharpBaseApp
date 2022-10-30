using app.presentation.validation;
using Godot;
using System;

public class AddBar : HBoxContainer
{
    [Signal]
    public delegate void added(string text);

    private LineEdit LineEditNewEntry;
    private Control Spacer;
    private Button ButtonAddEntry;
    private Button ButtonCancel;
    private InputFeedback InputErrorFeedback;

    protected bool _isAdding;
    public  bool IsAdding
    {
        get => _isAdding;
        protected set
        {
            this._isAdding = value;
            if (value)
            {
                this.LineEditNewEntry.Visible = true;
                this.LineEditNewEntry.Text = "";
                this.Spacer.Visible = false;
                this.ButtonAddEntry.Visible = false;
                this.ButtonCancel.Visible = true;
            }
            else
            {
                this.LineEditNewEntry.Visible = false;
                this.Spacer.Visible = true;
                this.ButtonAddEntry.Visible = true;
                this.ButtonCancel.Visible = false;
            }
        }
    }

    /// <summary>
    /// If not null, the validator used to validate user input. 
    /// </summary>
    public IValidator<string> Validator { get; set; }

    public override void _Ready()
    {
        this.LineEditNewEntry = GetNode<LineEdit>("Control/LineEditNewEntry");
        this.Spacer = GetNode<Control>("Spacer");
        this.ButtonAddEntry = GetNode<Button>("ButtonAddEntry");
        this.ButtonCancel = GetNode<Button>("ButtonCancel");
        this.InputErrorFeedback = GetNode<InputFeedback>("Control/InputErrorFeedback");

        this.IsAdding = false;
    }

    public void _on_ButtonAddTag_pressed()
    {
        this.IsAdding = true;
        this.LineEditNewEntry.GrabFocus();
    }

    public void _on_LineEditNewTag_focus_exited()
    {
        this.IsAdding = false;
        this.InputErrorFeedback.Hide();
    }

    public void _on_LineEditNewTag_gui_input(InputEvent @event)
    {
        if (!IsAdding)
            return;

        if (Input.IsActionPressed("ui_accept"))
        {
            string enteredText = this.LineEditNewEntry.Text;

            var validation = this.Validator.Validate(enteredText);
            if (validation.Success)
            {
                this.IsAdding = false;
                EmitSignal("added", enteredText);
            }
            else
            {
                this.InputErrorFeedback.Show(validation.Reason.Message);
            }
        }
        else if (Input.IsActionPressed("ui_cancel"))
        {
            this.IsAdding = false;
        }
    }

    public void _on_LineEditNewEntry_text_changed(string new_text)
    {
        this.InputErrorFeedback.Hide();
    }
}
