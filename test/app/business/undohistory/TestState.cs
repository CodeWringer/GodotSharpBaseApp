using app.business.state;
using app.business.util;
using System;
using System.Collections.Generic;

namespace test.app.business.undohistory
{
    internal class TestState : AbstractApplicationState<TestState>
    {
        public List<TestItem> Items;

        public TestState()
        {
            Items = new List<TestItem>();
        }

        public override void Apply(TestState toApply)
        {
            this.Items = toApply.Items;
        }

        public override TestState Clone()
        {
            var clonedItems = new List<TestItem>();
            foreach (var item in Items)
            {
                clonedItems.Add(item.Clone());
            }

            return new TestState()
            {
                Items = clonedItems
            };
        }
    }

    internal class TestItem : ITypedCloneable<TestItem>
    {
        public Guid Id;
        public string Name;

        public TestItem(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public TestItem(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public TestItem Clone()
        {
            return new TestItem(Id, Name);
        }
    }
}
