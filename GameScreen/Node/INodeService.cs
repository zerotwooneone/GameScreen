using System.Threading.Tasks;
using GameScreen.Location;

namespace GameScreen.Node
{
    public interface INodeService
    {
        Task<IPageable<LocationModel>> GetChildLocationsByParentId(string parentId, uint recursiveDepth = 1);
    }
}