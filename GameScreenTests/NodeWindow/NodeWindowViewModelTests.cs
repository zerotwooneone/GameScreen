using System;
using System.Reactive.Subjects;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameScreenTests.NodeWindow
{
    [TestClass]
    public class NodeWindowViewModelTests
    {
        private MockRepository mockRepository;

        private Mock<LocationViewmodel> mockLocationViewmodel;
        private Mock<INodeNavigationService> mockNodeNavigationService;
        private Mock<INodeViewmodelFactory> mockNodeViewmodelFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLocationViewmodel = this.mockRepository.Create<LocationViewmodel>();
            this.mockNodeNavigationService = this.mockRepository.Create<INodeNavigationService>();
            this.mockNodeViewmodelFactory = this.mockRepository.Create<INodeViewmodelFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private NodeWindowViewModel CreateViewModel()
        {
            return new NodeWindowViewModel(
                this.mockLocationViewmodel.Object,
                this.mockNodeNavigationService.Object,
                this.mockNodeViewmodelFactory.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var unitUnderTest = this.CreateViewModel();
            const string newLocationId = "5c9174b4a1effb00d8cba037";
            mockNodeNavigationService
                .Setup(mns => mns.NavigationObservable)
                .Returns(new BehaviorSubject<INavigationParam>(new NavigationParam(newLocationId, false)));

            // Act
            unitUnderTest.OnInitialized(unitUnderTest, EventArgs.Empty);
            
            // Assert
            Assert.Fail();
        }
    }
}
