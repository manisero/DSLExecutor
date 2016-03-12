using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Newtonsoft.Json;

namespace Manisero.DSLExecutor.Parser.Json
{
    public class JsonParser
    {
        public IExpression Parse(string json)
        {
            return JsonConvert.DeserializeObject<IExpression>(json);
        }
    }
}
