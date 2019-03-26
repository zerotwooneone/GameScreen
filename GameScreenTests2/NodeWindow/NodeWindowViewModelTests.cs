using System.Threading.Tasks;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
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
        private Mock<INodeService> _nodeService;
        private Mock<LocationViewmodel.Factory> _locationViewmodelFactory;
        private Mock<LocationListItemViewmodel.Factory> _locationListItemViewmodelFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNodeNavigationService = this.mockRepository.Create<INodeNavigationService>();
            _locationService = mockRepository.Create<ILocationService>();
            _dispatcherAccessor = new TestDispatcherAccessor(mockRepository);
            _nodeService = mockRepository.Create<INodeService>();
            _locationViewmodelFactory = mockRepository.Create<LocationViewmodel.Factory>();
            _locationListItemViewmodelFactory = mockRepository.Create<LocationListItemViewmodel.Factory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }
        

        private NodeWindowViewModel CreateViewModel(LocationModel locationModel = null,
            NodeHistoryState nodeHistoryState = null)
        {
            if (locationModel == null) locationModel = new LocationModel();
            return new NodeWindowViewModel(
                locationModel,
                this.mockNodeNavigationService.Object,
                _locationService.Object,
                _locationViewmodelFactory.Object,
                _dispatcherAccessor,
                nodeHistoryState);
        }

        private INavigationContext CreateNavigationContext(LocationModel locationModel = null)
        {
            return CreateViewModel(locationModel);
        }

        [TestMethod]
        public async Task Title_AfterGotoLocation_ExpectedValue()
        {
            // Arrange
            const string expected = "expected";
            var locationModel = new LocationModel{Name = "Initial"};
            var viewmodel = new LocationViewmodel(locationModel, null, null, null, null);
            _locationViewmodelFactory
                .Setup(lvf => lvf(It.IsAny<LocationModel>(), It.IsAny<INavigationContext>()))
                .Returns(viewmodel);
            var windowViewModel = this.CreateViewModel(locationModel: locationModel);
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
            var viewmodel = new LocationViewmodel(locationModel, null, null, null, null);
            var nodeHistoryState = new NodeHistoryState(new HistoryNode(locationModel.Name, locationModel.Id.ToString()), 1, new []{new HistoryNode("back node", "back node id"), });
            _locationViewmodelFactory
                .Setup(lvf => lvf(It.IsAny<LocationModel>(), It.IsAny<INavigationContext>()))
                .Returns(viewmodel);
            var unitUnderTest = this.CreateViewModel(locationModel: locationModel, nodeHistoryState: nodeHistoryState);
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
