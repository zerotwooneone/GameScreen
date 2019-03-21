using MongoDB.Bson;

namespace GameScreen.Location
{
    public class LocationModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public ObjectId? ParentLocationId { get; set; }
        public string DmMapUrl { get; set; }
        public string PlayerMapUrl { get; set; }
    }
}