using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Text.Json;

namespace Inventario
{
    public class GuidSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            return type == typeof(Guid) ? new GuidSerializer(GuidRepresentation.Standard) : null;
        }
    }
}
