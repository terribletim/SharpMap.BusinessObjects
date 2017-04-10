using GeoAPI.Geometries;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GeoJsonObjectModel;
using SharpMap.Converters;
using SharpMap.Data.Providers.Business;

namespace SharpMap.Business.Tests.MongoDB
{
    public class PoIRepository : MongoDbBusinessObjectSource<PoI, GeoJson2DCoordinates>
    {
        public PoIRepository(GeoJsonConverter<GeoJson2DCoordinates> converter, MongoClientSettings settings, string database, string collection) 
            : base(converter, settings, database, collection)
        {
        }

        public PoIRepository(GeoJsonConverter<GeoJson2DCoordinates> converter, string connectionString, string database, string collection) 
            : base(converter, connectionString, database, collection)
        {
        }


        protected override IMongoQuery BuildEnvelopeQuery(Envelope box)
        {
            //return Query<PoI>.GeoIntersects(t => t.BsonGeometry, Converter.ToPolygon(box));        
            return Query<PoI>.WithinRectangle(t => t.BsonGeometry, box.MinX, box.MinY, box.MaxX, box.MaxY);        
        }
    }
}