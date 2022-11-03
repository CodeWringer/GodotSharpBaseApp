using app.business.actionhistory.command;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.app.business.actionhistory.command
{
    internal class TestItem
    {
        public Guid Id;
        public string Name;

        public TestItem(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

    internal class TestModelState
    {
        public List<TestItem> Items;

        public TestModelState()
        {
            Items = new List<TestItem>();
        }
    }

    internal class AddItemCommand : IReversibleCommand
    {
        private TestItem _item;
        private TestModelState _state;
        private string _newName;

        public AddItemCommand(TestModelState state, string newName)
        {
            _state = state;
            _newName = newName;
        }

        public void Invoke()
        {
            if (_item == null)
                _item = new TestItem(_newName);

            _state.Items.Add(_item);
        }

        public void InvokeReverse()
        {
            _state.Items.Remove(_item);
        }
    }

    internal class RenameItemCommand : IReversibleCommand
    {
        private TestItem _item;
        private string _oldName;
        private string _newName;

        public RenameItemCommand(TestItem item, string newName)
        {
            _item = item;
            _newName = newName;
            _oldName = item.Name;
        }

        public void Invoke()
        {
            _item.Name = _newName;
        }

        public void InvokeReverse()
        {
            _item.Name = _oldName;
        }
    }

    internal class CommandHistoryTest
    {
        [Test]
        public void OneUndoLimit100()
        {
            // Given
            var given = new CommandHistory();
            given.MaxHistoryEntryCount = 100;

            var givenState = new TestModelState();
            var givenNewItemName = "Abc1";
            // When
            given.Invoke(new AddItemCommand(givenState, givenNewItemName));
            // Then
            Assert.AreEqual(1, given.Reversible.Count);
            Assert.AreEqual(0, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenNewItemName, givenState.Items.First().Name);
            // When
            var undone = given.Undo();
            // Then
            Assert.NotNull(undone);
            Assert.AreEqual(0, given.Reversible.Count);
            Assert.AreEqual(1, given.Reversed.Count);
            Assert.AreEqual(0, givenState.Items.Count);
            // When
            undone = given.Undo();
            // Then
            Assert.Null(undone);
        }

        [Test]
        public void OneUndoOneRedoLimit100()
        {
            // Given
            var given = new CommandHistory();
            given.MaxHistoryEntryCount = 100;

            var givenState = new TestModelState();
            var givenNewItemName1 = "Abc1";
            var givenNewItemName2 = "Def2";
            // When
            given.Invoke(new AddItemCommand(givenState, givenNewItemName1));
            var item = givenState.Items.First();
            given.Invoke(new RenameItemCommand(item, givenNewItemName2));
            // Then
            Assert.AreEqual(2, given.Reversible.Count);
            Assert.AreEqual(0, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenNewItemName2, givenState.Items.First().Name);
            // When
            var undone = given.Undo();
            // Then
            Assert.NotNull(undone);
            Assert.AreEqual(1, given.Reversible.Count);
            Assert.AreEqual(1, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenNewItemName1, givenState.Items.First().Name);
        }

        [Test]
        public void UndoRedoLimit100()
        {
            // Given
            var given = new CommandHistory();
            given.MaxHistoryEntryCount = 100;

            var givenState = new TestModelState();
            var givenNewItemName1 = "Abc1";
            var givenNewItemName2 = "Def2";
            // When
            given.Invoke(new AddItemCommand(givenState, givenNewItemName1));
            var item = givenState.Items.First();
            given.Invoke(new RenameItemCommand(item, givenNewItemName2));
            // Then
            Assert.AreEqual(2, given.Reversible.Count);
            Assert.AreEqual(0, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenNewItemName2, givenState.Items.First().Name);
            // When
            var undone = given.Undo();
            // Then
            Assert.NotNull(undone);
            Assert.AreEqual(1, given.Reversible.Count);
            Assert.AreEqual(1, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenNewItemName1, givenState.Items.First().Name);
            // When
            given.Undo();
            // Then
            Assert.AreEqual(0, given.Reversible.Count);
            Assert.AreEqual(2, given.Reversed.Count);
            Assert.AreEqual(0, givenState.Items.Count);
            // When
            given.Redo();
            given.Redo();
            // Then
            Assert.AreEqual(2, given.Reversible.Count);
            Assert.AreEqual(0, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenNewItemName2, givenState.Items.First().Name);
        }

        [Test]
        public void AnonymousCommands()
        {
            // Given
            var given = new CommandHistory();
            given.MaxHistoryEntryCount = 100;

            var givenState = new TestModelState();
            var givenName = "Abc1";
            // When
            given.Invoke(new ReversibleCommand<TestModelState>(givenState, 
                (state, workingData) => {
                    if (workingData.ContainsKey("item"))
                    {
                        var item = (TestItem)workingData["item"];
                        state.Items.Add(item);
                    }
                    else
                    {
                        var newItem = new TestItem(givenName);
                        state.Items.Add(newItem);
                        workingData.Add("item", newItem.Id);
                    }
                },
                (state, workingData) => {
                    var itemToRemove = (TestItem)workingData["item"];
                    state.Items.Remove(itemToRemove);
                }
            ));
            // Then
            Assert.AreEqual(1, given.Reversible.Count);
            Assert.AreEqual(0, given.Reversed.Count);
            Assert.AreEqual(1, givenState.Items.Count);
            Assert.AreEqual(givenN, givenState.Items.First().Name);
        }
    }
}
