using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
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
        private Mock<ILocationService> _locationService;
        private TestDispatcherAccessor _dispatcherAccessor;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLocationViewmodel = this.mockRepository.Create<LocationViewmodel>();
            this.mockNodeNavigationService = this.mockRepository.Create<INodeNavigationService>();
            _locationService = mockRepository.Create<ILocationService>();
            _dispatcherAccessor = new TestDispatcherAccessor(mockRepository);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private NodeWindowViewModel CreateViewModel(LocationViewmodel.Factory locationViewmodelFactory = null)
        {
            if(locationViewmodelFactory == null) locationViewmodelFactory = location => new LocationViewmodel(null, location, null, null);
            return new NodeWindowViewModel(
                this.mockLocationViewmodel.Object,
                this.mockNodeNavigationService.Object,
                _locationService.Object,
                locationViewmodelFactory,
                _dispatcherAccessor);
        }

        [TestMethod]
        public void OnInitialized_WithLocationNavigation_ReplacesLocationAsync()
        {
            // Arrange
            var locationModel = new LocationModel();
            var expected = new LocationViewmodel(null, locationModel, null, null);
            var unitUnderTest = this.CreateViewModel(location=>expected);
            const string newLocationId = "5c9174b4a1effb00d8cba037";
            mockNodeNavigationService
                .Setup(mns => mns.NavigationObservable)
                .Returns(new BehaviorSubject<INavigationParam>(new NavigationParam(newLocationId, false)));
            _locationService
                .Setup(ls => ls.GetLocationById(newLocationId))
                .Returns(Task.FromResult(locationModel));
            _dispatcherAccessor
                .SetupInvokeAction();

            // Act
            unitUnderTest.OnInitialized(unitUnderTest, EventArgs.Empty);
            
            // Assert
            Assert.AreEqual(expected ,unitUnderTest.Location);
        }

        [TestMethod]
        public void OnInitialized_WithNewWindowNavigation_LocationRemains()
        {
            var unitUnderTest = this.CreateViewModel();
            var expected = unitUnderTest.Location;
            const string newLocationId = "5c9174b4a1effb00d8cba037";
            mockNodeNavigationService
                .Setup(mns => mns.NavigationObservable)
                .Returns(new BehaviorSubject<INavigationParam>(new NavigationParam(newLocationId, true)));
            
            // Act
            unitUnderTest.OnInitialized(unitUnderTest, EventArgs.Empty);
            
            // Assert
            Assert.AreEqual(expected ,unitUnderTest.Location);
        }
    }
}
