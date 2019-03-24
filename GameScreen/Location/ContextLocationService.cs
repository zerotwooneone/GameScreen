using System.Collections.Generic;
using System.Threading.Tasks;
using GameScreen.MongoDb;

namespace GameScreen.Location
{
    public class ContextLocationService : ILocationService
    {
        private readonly IGameScreenContext _gameScreenContext;

        public ContextLocationService(IGameScreenContext gameScreenContext)
        {
            _gameScreenContext = gameScreenContext;
        }
        public Task OpenLocation(string locationId)
        {
            return _gameScreenContext.GetLocationById(locationId);
        }

        public Task<LocationModel> GetLocationById(string locationId)
        {
            //implement caching here?
            return _gameScreenContext.GetLocationById(locationId);
        }

        //public Task<IEnumerable<LocationModel>> GetLocationByParentId(string locationId, int pageSize, int pageIndex)
        //{
        //    //implement caching here?
        //    return _gameScreenContext.GetLocationsByParentId(locationId, pageSize, pageIndex);
        //}
    }
}