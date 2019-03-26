using System;
using System.Linq;
using System.Threading.Tasks;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.Pageable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameScreenTests2.Location
{
    [TestClass]
    public class LocationViewmodelTests
    {
        private MockRepository mockRepository;
        private Mock<INodeService> _nodeService;
        private Mock<INodeNavigationService> _nodeNavigationService;
        private Mock<LocationListItemViewmodel.Factory> _locationListItemViewmodelFactory;
        private Mock<INavigationContext> _navigationContext;


        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            _nodeService = mockRepository.Create<INodeService>();
            _nodeNavigationService = mockRepository.Create<INodeNavigationService>();
            _locationListItemViewmodelFactory = mockRepository.Create<LocationListItemViewmodel.Factory>();
            _navigationContext = mockRepository.Create<INavigationContext>();
        }

        private LocationViewmodel CreateViewmodel(LocationModel locationModel = null)
        {
            locationModel = locationModel ?? new LocationModel();
            return new LocationViewmodel(locationModel, _nodeService.Object, 
                _nodeNavigationService.Object, _navigationContext.Object, 
                _locationListItemViewmodelFactory.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }
        
        [TestMethod]
        public void Load_HasChildLocation_ListsChild()
        {
            // Arrange
            var unitUnderTest = this.CreateViewmodel();
            _nodeService
                .Setup(ns => ns.GetChildLocationsByParentId(It.IsAny<string>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<uint>()))
                .Returns(Task.FromResult(
                    (IPageable<LocationModel>) new PageableList<LocationModel>(
                        new[] {new LocationModel()}, 
                        false)));
            const string expected = "expected";
            _locationListItemViewmodelFactory
                .Setup(lif => lif(It.IsAny<string>(), It.IsAny<Action>(), It.IsAny<Action>()))
                .Returns(new LocationListItemViewmodel(expected, () => { }, () => { }));

            // Act
            unitUnderTest
                .LoadedCommand
                .Execute(null);
            var actual = unitUnderTest
                .Locations
                .First()
                .Name;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
