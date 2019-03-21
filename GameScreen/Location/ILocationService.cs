using System.Threading.Tasks;

namespace GameScreen.Location
{
    public interface ILocationService
    {
        Task OpenLocation(string locationId);
        Task<LocationModel> GetLocationById(string locationId);
    }
}