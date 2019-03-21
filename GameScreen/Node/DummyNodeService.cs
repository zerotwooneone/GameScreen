using System.Linq;
using System.Threading.Tasks;
using GameScreen.Location;

namespace GameScreen.Node
{
    public class DummyNodeService : INodeService
    {
        public Task<IPageable<LocationModel>> GetChildLocationsByParentId(string parentId, uint recursiveDepth = 1)
        {
            return Task.FromResult((IPageable<LocationModel>)new PageableList<LocationModel>(Enumerable.Empty<LocationModel>(), 10, false));
        }
    }
}