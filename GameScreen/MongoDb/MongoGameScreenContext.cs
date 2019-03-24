using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameScreen.Location;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameScreen.MongoDb
{
    public class MongoGameScreenContext : IGameScreenContext
    {
        public const string mongoId = "_id";
        private readonly IMongoDatabase _mongoDatabase;
        private readonly Lazy<IMongoCollection<LocationModel>> _locationCollection;
        public MongoGameScreenContext(IMongoClient mongoClient)
        {
            _mongoDatabase = mongoClient.GetDatabase("gameScreenDb");
            _locationCollection = new Lazy<IMongoCollection<LocationModel>>(()=>_mongoDatabase.GetCollection<LocationModel>("locations"));
        }
        public IMongoCollection<LocationModel> Locations => _locationCollection.Value;

        public async Task<LocationModel> GetLocationById(string locationId)
        {
            var query = Builders<LocationModel>
                .Filter
                .Eq(mongoId, ObjectId.Parse(locationId));
            return await Locations
                .Find(query)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LocationModel>> GetLocationsByParentId(string locationId, int pageSize, int pageIndex)
        {
            var query = Builders<LocationModel>
                .Filter
                .Eq("ParentLocationId", ObjectId.Parse(locationId));
            var skips = pageSize * pageIndex;
            return await Locations
                .Find(query)
                .Skip(skips)
                .Limit(pageSize)
                .ToListAsync();
        }
    }
}