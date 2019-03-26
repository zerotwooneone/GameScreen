using GameScreen.Location;
using System.Threading.Tasks;
using GameScreen.Pageable;

namespace GameScreen.Node
{
    public interface INodeService
    {
        Task<IPageable<LocationModel>> GetChildLocationsByParentId(string parentId, int pageSize, int pageIndex, uint recursiveDepth = 1);
    }
}