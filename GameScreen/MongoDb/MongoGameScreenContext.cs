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
            //var targetId = ObjectId.Parse(locationId);
            //var filterDefinition = Builders<LocationModel>
            //    .Filter
            //    .ElemMatch(lm=>lm.RelatedLocation, relatedLocationId=>relatedLocationId == targetId);
            //var skips = pageSize * pageIndex;
            //return await Locations
            //    .Find(filterDefinition)
            //    .Skip(skips)
            //    .Limit(pageSize)
            //    .ToListAsync();

            var targetIds = new[]{ ObjectId.Parse(locationId)};
            var filterDefinition = Builders<LocationModel>
                .Filter
                .AnyIn(lm => lm.RelatedLocation, targetIds);
            var skips = pageSize * pageIndex;
            return await Locations
                .Find(filterDefinition)
                .Skip(skips)
                .Limit(pageSize)
                .ToListAsync();

        }

        public async Task AddLocations(IEnumerable<LocationModel> locationModels)
        {
            await Locations.InsertManyAsync(locationModels);
        }
    }
}