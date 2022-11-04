using app.business.actionhistory.command;
using app.business.model;
using Godot;
using System;
using System.Collections.Generic;

public class DemoUndoHistory : Control
{
    // Undo history
    private const string PATH_CONTAINER_UNDO = "VBoxContainerUndo";

    // Command undo history
    private const string PATH_CONTAINER_UNDO_COMMAND = PATH_CONTAINER_UNDO + "/VBoxContainerCommand";
    private const string PATH_CONTAINER_UNDO_CONTROLS_COMMAND = PATH_CONTAINER_UNDO_COMMAND + "/ControlsListCommand";
    private ItemList _itemListCommand;
    private Button _buttonAddItemCommand;
    private Button _buttonRenameItemCommand;
    private Button _buttonDeleteItemCommand;
    private Button _buttonUndoCommand;
    private Button _buttonRedoCommand;
    private Label _labelInfoCommand;
    private List<AnItem> _itemsCommand;
    private CommandHistory _commandHistory;

    public override void _Ready()
    {
        _itemListCommand = GetNode<ItemList>(PATH_CONTAINER_UNDO_COMMAND + "/ItemList");
        _buttonAddItemCommand = GetNode<Button>(PATH_CONTAINER_UNDO_CONTROLS_COMMAND + "/ButtonAdd");
        _buttonAddItemCommand.Connect("pressed", this, nameof(_on_buttonAddCommand_pressed));
        _buttonRenameItemCommand = GetNode<Button>(PATH_CONTAINER_UNDO_CONTROLS_COMMAND + "/ButtonRename");
        _buttonRenameItemCommand.Connect("pressed", this, nameof(_on_buttonRenameCommand_pressed));
        _buttonDeleteItemCommand = GetNode<Button>(PATH_CONTAINER_UNDO_CONTROLS_COMMAND + "/ButtonDelete");
        _buttonDeleteItemCommand.Connect("pressed", this, nameof(_on_buttonDeleteCommand_pressed));
        _buttonUndoCommand = GetNode<Button>(PATH_CONTAINER_UNDO_CONTROLS_COMMAND + "/ButtonUndo");
        _buttonUndoCommand.Connect("pressed", this, nameof(_on_buttonUndoCommand_pressed));
        _buttonRedoCommand = GetNode<Button>(PATH_CONTAINER_UNDO_CONTROLS_COMMAND + "/ButtonRedo");
        _buttonRedoCommand.Connect("pressed", this, nameof(_on_buttonRedoCommand_pressed));
        _labelInfoCommand = GetNode<Label>(PATH_CONTAINER_UNDO_CONTROLS_COMMAND + "/LabelInfo");
        _itemsCommand = new List<AnItem>()
        {
            new AnItem("Abc1")
        };
        _commandHistory = new CommandHistory();
    }

    public void _on_buttonAddCommand_pressed()
    {
        _commandHistory.Invoke(new ReversibleCommand<List<AnItem>>(_itemsCommand,
            (state, workingData) => {
                AnItem item;
                string newName = "New Item";

                if (workingData.ContainsKey("itemId"))
                    item = new AnItem((Guid)workingData["itemId"], newName);
                else
                    item = new AnItem(newName);

                state.Add(item);
            },
            (state, workingData) => {
                Guid id = (Guid)workingData["itemId"];
                var toRemove = state.Find(item => item.Id == id);
                state.Remove(toRemove);
            }
        ));
    }

    public void _on_buttonRenameCommand_pressed()
    {

    }

    public void _on_buttonDeleteCommand_pressed()
    {

    }

    public void _on_buttonUndoCommand_pressed()
    {
        _commandHistory.Undo();
        _labelInfoCommand.Text = string.Format("Undo-able: {0}; Redo-able: {1}", _commandHistory.Reversible.Count, _commandHistory.Reversed.Count);
    }

    public void _on_buttonRedoCommand_pressed()
    {
        _commandHistory.Redo();
        _labelInfoCommand.Text = string.Format("Undo-able: {0}; Redo-able: {1}", _commandHistory.Reversible.Count, _commandHistory.Reversed.Count);
    }
}
