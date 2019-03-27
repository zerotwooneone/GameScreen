using GameScreen;
using GameScreen.Location;
using GameScreen.Node;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using GameScreen.Navigation;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;
using MongoDB.Bson;

namespace GameScreenTests2
{
    [TestClass]
    public class MainWindowViewmodelTests
    {
        private MockRepository mockRepository;

        private Mock<ILocationService> mockLocationService;
        private Mock<INodeNavigationService> mockNodeNavigationService;
        private Mock<NodeWindowViewModel.LocationFactory> _nodeWindowLocationFactory;
        private Mock<LocationViewmodel.Factory> _locationViewmodelFactory;
        private Mock<IWindowService> _windowService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLocationService = this.mockRepository.Create<ILocationService>();
            this.mockNodeNavigationService = this.mockRepository.Create<INodeNavigationService>();

            _nodeWindowLocationFactory = mockRepository.Create<NodeWindowViewModel.LocationFactory>();
            _locationViewmodelFactory = mockRepository.Create<LocationViewmodel.Factory>();
            _windowService = mockRepository.Create<IWindowService>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private MainWindowViewmodel CreateMainWindowViewmodel()
        {
            return new MainWindowViewmodel(
                _nodeWindowLocationFactory.Object,
                _locationViewmodelFactory.Object,
                this.mockLocationService.Object,
                this.mockNodeNavigationService.Object,
                _windowService.Object);
        }

        [TestMethod]
        public void Loaded_OpenNewRequest_()
        {
            // Arrange
            var unitUnderTest = this.CreateMainWindowViewmodel();
            mockNodeNavigationService
                .SetupGet(nns => nns.NavigationObservable)
                .Returns(new BehaviorSubject<INavigationParam>(new NavigationParam(null, true)));
            _windowService
                .Setup(ws => ws.OpenNewNode(It.IsAny<NodeWindowViewModel>()))
                .Returns(Task.CompletedTask);
            var newLocationModel = new LocationModel
            {
                Name = "something",
                Id = ObjectId.Empty
            };
            mockLocationService
                .Setup(ls => ls.GetLocationById(It.IsAny<string>()))
                .Returns(Task.FromResult(newLocationModel));
            var expected = new NodeWindowViewModel(newLocationModel, null, null, (x,y)=>null, null, null);
            _nodeWindowLocationFactory
                .Setup(lvf => lvf(It.IsAny<LocationModel>(), It.IsAny<NodeHistoryState>()))
                .Returns(expected);

            // Act
            unitUnderTest
                .LoadedCommand
                .Execute(null);

            // Assert
            _windowService
                .Verify(ws => ws.OpenNewNode(expected));
        }
    }
}
