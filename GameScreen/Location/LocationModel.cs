using System.Collections.Generic;
using MongoDB.Bson;

namespace GameScreen.Location
{
    public class LocationModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string DmMapUrl { get; set; }
        public string PlayerMapUrl { get; set; }
        public List<ObjectId> RelatedLocation { get; set; }
    }
}