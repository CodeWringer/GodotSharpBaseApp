using app.business.util;
using NUnit.Framework;

namespace test.app.business.util
{
    public class LimitedStackTest
    {
        [Test]
        public void PushRespectsLimit()
        {
            // Given
            var given = new LimitedStack<string>(3);
            // When
            given.Push("ab");
            given.Push("cd");
            given.Push("ef");
            given.Push("gh");
            given.Push("ij");
            // Then
            Assert.AreEqual(3, given.Count);
            Assert.AreEqual("ef", given[0]);
            Assert.AreEqual("gh", given[1]);
            Assert.AreEqual("ij", given[2]);
            Assert.AreEqual("ij", given.Peek());
        }

        [Test]
        public void LoweringLimitDiscardsOldestEntries()
        {
            // Given
            var given = new LimitedStack<string>(5);
            // When
            given.Push("ab");
            given.Push("cd");
            given.Push("ef");
            given.Push("gh");
            given.Push("ij");
            // Then
            Assert.AreEqual(5, given.Count);
            // When
            given.Capacity = 3;
            // Then
            Assert.AreEqual(3, given.Count);
            Assert.AreEqual("ef", given[0]);
            Assert.AreEqual("gh", given[1]);
            Assert.AreEqual("ij", given[2]);
            Assert.AreEqual("ij", given.Peek());
        }

        [Test]
        public void RaisingLimitDoesNotDiscardOldEntries()
        {
            // Given
            var given = new LimitedStack<string>(3);
            // When
            given.Push("ab");
            given.Push("cd");
            given.Push("ef");
            // Then
            Assert.AreEqual(3, given.Count);
            // When
            given.Capacity = 5;
            given.Push("gh");
            given.Push("ij");
            // Then
            Assert.AreEqual(5, given.Count);
            Assert.AreEqual("ab", given[0]);
            Assert.AreEqual("cd", given[1]);
            Assert.AreEqual("ef", given[2]);
            Assert.AreEqual("gh", given[3]);
            Assert.AreEqual("ij", given[4]);
            Assert.AreEqual("ij", given.Peek());
        }
    }
}