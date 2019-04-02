using System.Collections.Generic;
using System.Threading.Tasks;
using GameScreen.Location;

namespace GameScreen.MongoDb
{
    public interface IGameScreenContext
    {
        Task<LocationModel> GetLocationById(string locationId);
        Task<IEnumerable<LocationModel>> GetLocationsByParentId(string locationId, int pageSize, int pageIndex);
        Task AddLocations(IEnumerable<LocationModel> locationModels);
    }
}