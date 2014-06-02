using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ioget
{
    public class Instantiator
    {
        private readonly List<Conversion<object>> conversions;

        public Instantiator()
        {
            conversions = new List<Conversion<object>>
            {
                new ByteConversion(),
                new ShortConversion(),
                new IntConversion(), 
                new LongConversion(),
                new FloatConversion(),
                new DoubleConversion(),
                new BooleanConversion(),
                new StringConversion(),
                new EnumConversion(),
                new CharConversion()
            };
        }

        public T Bind<T>(Dictionary<string, string> dic)
        {
            var typeParameterType = typeof(T);
            var constructor = typeParameterType.GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Count() == dic.Count);

            if (constructor == null)
                throw new MissingConstructor("Could not find constructor for parameters dictionary");

            var parameters = constructor.GetParameters().Select(param =>
            {
                var conversions = ConversionFor(param);
                if (conversions.Count == 0)
                {
                    var newDic = dic.
                        Where(kv => kv.Key.StartsWith(param.Name + ".")).
                        Select(kv => new KeyValuePair<string, string>(kv.Key.Substring(param.Name.Length + 1), kv.Value)).
                        ToDictionary(kv => kv.Key, kv => kv.Value);


                    var bindMethod = typeof(Instantiator).GetMethod("Bind");
                    var genericRef = bindMethod.MakeGenericMethod(param.ParameterType);
                    return genericRef.Invoke(this, new object[] { newDic });
                }
                if (!dic.ContainsKey(param.Name))
                    throw new MissingParameterKey("Could not find key for parameter " + param.Name);

                return conversions.First().Apply(param, dic[param.Name]);
            });
            return (T)constructor.Invoke(parameters.ToArray());
        }

        private List<Conversion<object>> ConversionFor(ParameterInfo info)
        {
            return conversions.Where(c => c.IsApplicable(info)).ToList();
        }
    }

    public class MissingParameterKey : Exception
    {
        public MissingParameterKey(string message) : base(message) { }
    }

    public class MissingConstructor : Exception
    {
        public MissingConstructor(string message) : base(message) { }
    }
}