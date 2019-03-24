using System.Linq;
using System.Threading.Tasks;
using GameScreen.Location;
using GameScreen.MongoDb;

namespace GameScreen.Node
{
    public class ContextNodeService : INodeService
    {
        private readonly IGameScreenContext _gameScreenContext;

        public ContextNodeService(IGameScreenContext gameScreenContext)
        {
            _gameScreenContext = gameScreenContext;
        }
        public async Task<IPageable<LocationModel>> GetChildLocationsByParentId(string parentId, int pageSize, int pageIndex, uint recursiveDepth = 1)
        {
            var locations = (await _gameScreenContext.GetLocationsByParentId(parentId, pageSize+1, pageIndex)).ToArray();
            var hasMore = locations.Length > pageSize;
            var result = new PageableList<LocationModel>(locations.Take(pageSize),pageSize, hasMore);
            return result;
        }
    }
}