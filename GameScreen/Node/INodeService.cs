using GameScreen.Location;
using System.Threading.Tasks;

namespace GameScreen.Node
{
    public interface INodeService
    {
        Task<IPageable<LocationModel>> GetChildLocationsByParentId(string parentId, int pageSize, int pageIndex, uint recursiveDepth = 1);
    }
}