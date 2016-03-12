using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Newtonsoft.Json;

namespace Manisero.DSLExecutor.Parser.Json
{
    public interface IJsonParser
    {
        IExpression Parse(string json);
    }

    public class JsonParser : IJsonParser
    {
        private readonly Lazy<JsonSerializerSettings> _serializerSettigns = new Lazy<JsonSerializerSettings>(InitializeSerializerSettigns);

        public IExpression Parse(string json)
        {
            return JsonConvert.DeserializeObject<IExpression>(json, _serializerSettigns.Value);
        }

        private static JsonSerializerSettings InitializeSerializerSettigns()
        {
            return new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
        }
    }
}
