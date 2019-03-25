using System.Threading.Tasks;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameScreenTests.NodeWindow
{
    [TestClass]
    public class NodeWindowViewModelTests
    {
        private MockRepository mockRepository;

        private Mock<INodeNavigationService> mockNodeNavigationService;
        private Mock<ILocationService> _locationService;
        private TestDispatcherAccessor _dispatcherAccessor;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNodeNavigationService = this.mockRepository.Create<INodeNavigationService>();
            _locationService = mockRepository.Create<ILocationService>();
            _dispatcherAccessor = new TestDispatcherAccessor(mockRepository);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private NodeWindowViewModel CreateViewModel(LocationModel locationModel = null,
            LocationViewmodel.Factory locationViewmodelFactory = null,
            NodeHistoryState nodeHistoryState = null)
        {
            if (locationViewmodelFactory == null) locationViewmodelFactory = (location, navContext) => new LocationViewmodel(location, null,null, navContext);
            if (locationModel == null) locationModel = new LocationModel();
            return new NodeWindowViewModel(
                locationModel,
                this.mockNodeNavigationService.Object,
                _locationService.Object,
                locationViewmodelFactory,
                _dispatcherAccessor,
                nodeHistoryState);
        }

        private INavigationContext CreateNavigationContext(LocationModel locationModel = null,
        LocationViewmodel.Factory locationViewmodelFactory = null)
        {
            return CreateViewModel(locationModel, locationViewmodelFactory);
        }

        [TestMethod]
        public async Task Title_AfterGotoLocation_ExpectedValue()
        {
            // Arrange
            const string expected = "expected";
            var locationModel = new LocationModel{Name = "Initial"};
            var viewmodel = new LocationViewmodel(locationModel, null, null, null);
            var windowViewModel = this.CreateViewModel(locationModel: locationModel, locationViewmodelFactory: (location, navContext) => viewmodel);
            var unitUnderTest = (INavigationContext) windowViewModel;
            _locationService
                .Setup(ls => ls.GetLocationById(It.IsAny<string>()))
                .Returns(Task.FromResult(new LocationModel {Name = expected}));

            // Act
            await unitUnderTest.GoToLocation("some location id");
            var actual = windowViewModel.Title;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GoBack_HasBackHistory_LocationChanged()
        {
            // Arrange
            var locationModel = new LocationModel{Name = "Initial"};
            var viewmodel = new LocationViewmodel(locationModel, null, null, null);
            var nodeHistoryState = new NodeHistoryState(new HistoryNode(locationModel.Name, locationModel.Id.ToString()), 1, new []{new HistoryNode("back node", "back node id"), });
            var unitUnderTest = this.CreateViewModel(locationModel: locationModel, locationViewmodelFactory: (location, navContext) => viewmodel,
                nodeHistoryState: nodeHistoryState);
            const string expected = "expected";
            _locationService
                .Setup(ls => ls.GetLocationById(It.IsAny<string>()))
                .Returns(Task.FromResult(new LocationModel {Name = expected}));

            // Act
            unitUnderTest
                .BackCommand
                .Execute(null);
            var actual = unitUnderTest.Title;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
