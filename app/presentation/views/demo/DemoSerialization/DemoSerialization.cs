using app.business.dataaccess.common.datasource;
using app.business.dataaccess.example.dto;
using app.business.dataaccess.example;
using app.business.dataaccess.util;
using Godot;
using System;
using System.IO;
using System.Linq;
using app.business.state;

public class DemoSerialization : Control
{
    private ApplicationState _state;

    private Button _buttonSerialize;
    private Button _buttonDeserialize;
    private InputFeedback _deserializeFeedback;
    private Label _labelSerialization;
    private CheckButton _checkButtonToggleMock;
    private LineEdit _lineEditAbsolutePath;
    private Button _buttonSerialize2;
    private Button _buttonDeserialize2;
    private InputFeedback _deserializeFeedback2;
    private Label _labelSerialization2;

    private const string PATH_CONTAINER_SERIALIZATION = "VBoxContainerSerialization";

    public override void _Ready()
    {
        _state = ApplicationState.GetFromSceneTree(this);

        _buttonSerialize = GetNode<Button>(PATH_CONTAINER_SERIALIZATION + "/ButtonSerialize");
        _buttonSerialize.Connect("pressed", this, nameof(_on_buttonSerialize_pressed));

        _buttonDeserialize = GetNode<Button>(PATH_CONTAINER_SERIALIZATION + "/ButtonDeserialize");
        _buttonDeserialize.Connect("pressed", this, nameof(_on_buttonDeserialize_pressed));

        _deserializeFeedback = GetNode<InputFeedback>(PATH_CONTAINER_SERIALIZATION + "/ButtonDeserialize/InputFeedback");
        _labelSerialization = GetNode<Label>(PATH_CONTAINER_SERIALIZATION + "/LabelSerialization");

        _checkButtonToggleMock = GetNode<CheckButton>(PATH_CONTAINER_SERIALIZATION + "/HBoxContainerToggleMock/CheckButtonToggleMock");
        _lineEditAbsolutePath = GetNode<LineEdit>(PATH_CONTAINER_SERIALIZATION + "/HBoxContainerAbsolute/LineEditAbsolutePath");

        _buttonSerialize2 = GetNode<Button>(PATH_CONTAINER_SERIALIZATION + "/ButtonSerialize2");
        _buttonSerialize2.Connect("pressed", this, nameof(_on_buttonSerialize2_pressed));

        _buttonDeserialize2 = GetNode<Button>(PATH_CONTAINER_SERIALIZATION + "/ButtonDeserialize2");
        _buttonDeserialize2.Connect("pressed", this, nameof(_on_buttonDeserialize2_pressed));

        _deserializeFeedback2 = GetNode<InputFeedback>(PATH_CONTAINER_SERIALIZATION + "/ButtonDeserialize2/InputFeedback");
        _labelSerialization2 = GetNode<Label>(PATH_CONTAINER_SERIALIZATION + "/LabelSerialization2");
    }

    public void _on_buttonSerialize_pressed()
    {
        bool mock = _checkButtonToggleMock.Pressed;
        var repository = new ExampleGodotFileSystemRepository(mock);
        var toSerialize = new ExampleSerializable()
        {
            items = _state.Items
        };
        repository.Write(toSerialize);
        _labelSerialization.Text = "Serialized: " + toSerialize.items.Count() + " items";
    }

    public void _on_buttonDeserialize_pressed()
    {
        bool mock = _checkButtonToggleMock.Pressed;
        var repository = new ExampleGodotFileSystemRepository(mock);
        try
        {
            var deserialized = repository.Read();
            _labelSerialization.Text = "Deserialized: " + deserialized.items.Count() + " items";
        }
        catch (FileNotFoundException)
        {
            // This exception is "fine", because it is only thrown if the file does not yet exist. 
            // Therefore a non-critical issue, because the "Serialize" button can create it. 
            // In this case, we'll show the user they made a simple mistake in the order of operations. 
            _deserializeFeedback.Show("Could not deserialize - no file to deserialize exists, yet!");
        }
        catch (Exception e)
        {
            _deserializeFeedback.Show(e.Message);
            _labelSerialization.Text = string.Empty;
            return;
        }
    }

    public void _on_buttonSerialize2_pressed()
    {
        var filePath = _lineEditAbsolutePath.Text;
        if (PathUtility.IsFilePathValid(filePath) != true)
        {
            _deserializeFeedback2.Show("Invalid file path!");
            _labelSerialization2.Text = string.Empty;
            return;
        }

        var ds = new FileSystemDataSource<ExampleSerializable>(filePath);
        var toSerialize = new ExampleSerializable()
        {
            items = _state.Items
        };

        try
        {
            ds.Write(toSerialize);
        }
        catch (Exception e)
        {
            _deserializeFeedback2.Show(e.Message);
            _labelSerialization2.Text = string.Empty;
            return;
        }
        _labelSerialization2.Text = "Serialized: " + toSerialize.items.Count() + " items";
    }

    public void _on_buttonDeserialize2_pressed()
    {
        var filePath = _lineEditAbsolutePath.Text;
        if (PathUtility.IsFilePathValid(filePath) != true)
        {
            _deserializeFeedback2.Show("Invalid file path!");
            _labelSerialization2.Text = string.Empty;
            return;
        }

        var ds = new FileSystemDataSource<ExampleSerializable>(filePath);
        try
        {
            var deserialized = ds.Read();
            _labelSerialization2.Text = "Deserialized: " + deserialized.items.Count() + " items";
        }
        catch (FileNotFoundException)
        {
            // This exception is "fine", because it is only thrown if the file does not yet exist. 
            // Therefore a non-critical issue, because the "Serialize" button can create it. 
            // In this case, we'll show the user they made a simple mistake in the order of operations. 
            _deserializeFeedback.Show("Could not deserialize - no file to deserialize exists, yet!");
        }
        catch (Exception e)
        {
            _deserializeFeedback2.Show(e.Message);
            _labelSerialization2.Text = string.Empty;
            return;
        }
    }
}
