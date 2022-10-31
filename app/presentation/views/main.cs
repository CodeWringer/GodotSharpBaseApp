using app.business.dataaccess.example;
using app.business.model;
using app.business.state;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// The entry point to the application. 
/// </summary>
public class Main : Control
{
    private Button _buttonSerialize;
    private Button _buttonDeserialize;
    private InputFeedback _deserializeFeedback;
    private Label _labelSerialization;

    private const string PATH_CONTAINER_SERIALIZATION = "ScrollContainer/VBoxContainer/VBoxContainerSerialization";

    public override void _Ready()
    {
        _buttonSerialize = GetNode<Button>(PATH_CONTAINER_SERIALIZATION + "/ButtonSerialize");
        _buttonSerialize.Connect("pressed", this, "_on_buttonSerialize_pressed");

        _buttonDeserialize = GetNode<Button>(PATH_CONTAINER_SERIALIZATION + "/ButtonDeserialize");
        _buttonDeserialize.Connect("pressed", this, "_on_buttonDeserialize_pressed");

        _deserializeFeedback = GetNode<InputFeedback>(PATH_CONTAINER_SERIALIZATION + "/ButtonDeserialize/InputFeedback");
        _labelSerialization = GetNode<Label>(PATH_CONTAINER_SERIALIZATION + "/LabelSerialization");
    }

    public void _on_buttonSerialize_pressed()
    {
        var serializer = new ExampleGodotFilesystemSerializer();
        var toSerialize = new ExampleSerializable()
        {
            items = new List<AnItem>()
            {
                new AnItem("Abc1"),
                new AnItem("Def2"),
                new AnItem("Ghi3"),
            }
        };
        serializer.Write(toSerialize);
        _labelSerialization.Text = "Serialized: " + toSerialize.items.Count() + " items";
    }

    public void _on_buttonDeserialize_pressed()
    {
        var serializer = new ExampleGodotFilesystemSerializer();
        try
        {
            var deserialized = serializer.Read();
            _labelSerialization.Text = "Deserialized: " + deserialized.items.Count() + " items";
        }
        catch (FileNotFoundException)
        {
            // This exception is "fine", because it is only thrown if the file does not yet exist. 
            // Therefore a non-critical issue, because the "Serialize" button can create it. 
            // In this case, we'll show the user they made a simple mistake in the order of operations. 
            _deserializeFeedback.Show("Could not deserialize - no file to deserialize exists, yet!");
        }
    }
}
