using GameScreen.NodeHistory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GameScreenTests2.NodeHistory
{
    [TestClass]
    public class NodeHistoryStateTests
    {
        private MockRepository mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Append_BackEmpty_BackSet()
        {
            // Arrange
            const string expected = "First";
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode(expected, "location1"), 
                1,
                null,
                null);
            HistoryNode current = new HistoryNode("new", "location2");

            // Act
            var actual = unitUnderTest
                .Append(current)
                .BackNodes
                .Last()
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Append_BackHasMax_BackMaximumMaintained()
        {
            // Arrange
            const int expected = 1;
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode("First", "location1"), 
                expected,
                new []{new HistoryNode("back node", "back location"), },
                null);
            HistoryNode current = new HistoryNode("new", "location2");

            // Act
            var actual = unitUnderTest
                .Append(current)
                .BackNodes
                .Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Append_BackEmpty_CurrentSet()
        {
            // Arrange
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode("First", "location1"), 
                1,
                null,
                null);
            const string expected = "new";
            HistoryNode current = new HistoryNode(expected, "location2");

            // Act
            var actual = unitUnderTest
                .Append(current)
                .Current
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoBack_OneBack_CurrentSet()
        {
            // Arrange
            const string expected = "back node";
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode("First", "location1"), 
                1,
                new []{new HistoryNode(expected, "back location"), },
                null);
            
            // Act
            var actual = unitUnderTest
                .GoBack()
                .Current
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoBack_OneBack_BackEmpty()
        {
            // Arrange
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode("First", "location1"), 
                1,
                new []{new HistoryNode("back node", "back location"), },
                null);
            const int expected = 0;

            // Act
            var actual = unitUnderTest
                .GoBack()
                .BackNodes
                .Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoBack_OneBack_CurrentBecomesForward()
        {
            // Arrange
            const string expected = "First";
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode(expected, "location1"), 
                1,
                new []{new HistoryNode("back node", "back location"), },
                null);

            // Act
            var actual = unitUnderTest
                .GoBack()
                .ForwardNodes
                .First()
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void GoForward_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = this.CreateNodeHistoryState();

        //    // Act
        //    var result = unitUnderTest.GoForward();

        //    // Assert
        //    Assert.Fail();
        //}

        [TestMethod]
        public void GoBackTo_OneBack_CurrentBecomesForward()
        {
            // Arrange
            const string expected = "First";
            var backNode = new HistoryNode("back node", "back location");
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode(expected, "location1"), 
                1,
                new []{backNode, },
                null);

            // Act
            var actual = unitUnderTest
                .GoBackTo(backNode)
                .ForwardNodes
                .First()
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoBackTo_OneBack_CurrentSet()
        {
            // Arrange
            const string expected = "back node";
            var backNode = new HistoryNode(expected, "back location");
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode("First", "location1"), 
                1,
                new []{backNode, },
                null);
            
            // Act
            var actual = unitUnderTest
                .GoBackTo(backNode)
                .Current
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoBackTo_TwoBack_CurrentSet()
        {
            // Arrange
            const string expected = "back node";
            var backNode = new HistoryNode(expected, "back location");
            var unitUnderTest = new NodeHistoryState(
                new HistoryNode("First", "location1"), 
                1,
                new []{backNode, new HistoryNode("some other node","some other location id"),  },
                null);
            
            // Act
            var actual = unitUnderTest
                .GoBackTo(backNode)
                .Current
                .NodeName;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
