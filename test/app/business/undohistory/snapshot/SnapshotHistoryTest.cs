using app.business.actionhistory.state;
using NUnit.Framework;
using System;

namespace test.app.business.undohistory.state
{
    internal class SnapshotHistoryTest
    {
        [Test]
        public void UndoAndRedo()
        {
            // Given
            var givenState = new TestState();
            var given = new SnapshotHistory<TestState>(givenState);

            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            var givenNewItemName1 = "Abc1";
            var givenNewItemName2 = "Def2";
            // When
            givenState.Items.Add(new TestItem(id1, givenNewItemName1));
            given.TakeSnapshot();
            givenState.Items.Add(new TestItem(id2, givenNewItemName2));
            given.TakeSnapshot();
            // Then
            Assert.AreEqual(2, givenState.Items.Count);
            Assert.AreEqual(id1, givenState.Items[0].Id);
            Assert.AreEqual(givenNewItemName1, givenState.Items[0].Name);
            Assert.AreEqual(id2, givenState.Items[1].Id);
            Assert.AreEqual(givenNewItemName2, givenState.Items[1].Name);

            given.Undo();
            Assert.AreEqual(1, givenState.Items.Count);

            given.Redo();
            Assert.AreEqual(2, givenState.Items.Count);
            Assert.AreEqual(id1, givenState.Items[0].Id);
            Assert.AreEqual(givenNewItemName1, givenState.Items[0].Name);
            Assert.AreEqual(id2, givenState.Items[1].Id);
            Assert.AreEqual(givenNewItemName2, givenState.Items[1].Name);

            given.Undo();
            given.Undo();
            Assert.AreEqual(0, givenState.Items.Count);
        }

        [Test]
        public void CapacityLoweredByOneDropsOldestEntry()
        {
            var givenState = new TestState();
            var given = new SnapshotHistory<TestState>(givenState);
            given.MaxHistoryEntryCount = 2;

            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            var givenNewItemName1 = "Abc1";
            var givenNewItemName2 = "Def2";
            // When
            givenState.Items.Add(new TestItem(id1, givenNewItemName1));
            given.TakeSnapshot();
            givenState.Items.Add(new TestItem(id2, givenNewItemName2));
            given.TakeSnapshot();
            // Then
            Assert.AreEqual(2, given.Reversible.Count);
            Assert.AreEqual(2, givenState.Items.Count);
            Assert.AreEqual(id1, givenState.Items[0].Id);
            Assert.AreEqual(givenNewItemName1, givenState.Items[0].Name);
            Assert.AreEqual(id2, givenState.Items[1].Id);
            Assert.AreEqual(givenNewItemName2, givenState.Items[1].Name);
            // When
            given.MaxHistoryEntryCount = 1;
            // Then
            Assert.AreEqual(1, given.Reversible.Count);
            Assert.AreEqual(2, givenState.Items.Count);
            Assert.AreEqual(id1, givenState.Items[0].Id);
            Assert.AreEqual(givenNewItemName1, givenState.Items[0].Name);
            Assert.AreEqual(id2, givenState.Items[1].Id);
            Assert.AreEqual(givenNewItemName2, givenState.Items[1].Name);
        }
    }
}
