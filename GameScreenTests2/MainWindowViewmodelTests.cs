using GameScreen;
using GameScreen.Location;
using GameScreen.Node;
using GameScreen.Primary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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

        private Mock<Func<PrimaryWindow>> mockFunc;
        private Mock<ILocationService> mockLocationService;
        private Mock<INodeNavigationService> mockNodeNavigationService;
        private Mock<NodeWindowViewModel.LocationFactory> _nodeWindowLocationFactory;
        private Mock<LocationViewmodel.Factory> _locationViewmodelFactory;
        private Mock<NodeWindow.Factory> _nodeWindowFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockFunc = this.mockRepository.Create<Func<PrimaryWindow>>();
            this.mockLocationService = this.mockRepository.Create<ILocationService>();
            this.mockNodeNavigationService = this.mockRepository.Create<INodeNavigationService>();

            _nodeWindowLocationFactory = mockRepository.Create<NodeWindowViewModel.LocationFactory>();
            _locationViewmodelFactory = mockRepository.Create<LocationViewmodel.Factory>();
            _nodeWindowFactory = mockRepository.Create<NodeWindow.Factory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private MainWindowViewmodel CreateMainWindowViewmodel()
        {
            return new MainWindowViewmodel(
                this.mockFunc.Object,
                _nodeWindowLocationFactory.Object,
                _locationViewmodelFactory.Object,
                this.mockLocationService.Object,
                this.mockNodeNavigationService.Object,
                _nodeWindowFactory.Object);
        }

        [TestMethod]
        public void Loaded_OpenNewRequest_()
        {
            // Arrange
            var unitUnderTest = this.CreateMainWindowViewmodel();
            mockNodeNavigationService
                .SetupGet(nns => nns.NavigationObservable)
                .Returns(new BehaviorSubject<INavigationParam>(new NavigationParam(null, true)));
            var newNodeWindow = mockRepository.Create<NodeWindow>();
            _nodeWindowFactory
                .Setup(wlf => wlf(It.IsAny<NodeWindowViewModel>()))
                .Returns(newNodeWindow.Object);
            var newLocationModel = new LocationModel
            {
                Name = "something",
                Id = ObjectId.Empty
            };
            mockLocationService
                .Setup(ls => ls.GetLocationById(It.IsAny<string>()))
                .Returns(Task.FromResult(newLocationModel));
            _nodeWindowLocationFactory
                .Setup(lvf => lvf(It.IsAny<LocationModel>(), It.IsAny<NodeHistoryState>()))
                .Returns(new NodeWindowViewModel(newLocationModel, null, null, (x,y)=>null, null, null));

            // Act
            unitUnderTest
                .LoadedCommand
                .Execute(null);

            // Assert
            newNodeWindow.Verify(nw=>nw.Show());
        }
    }
}
