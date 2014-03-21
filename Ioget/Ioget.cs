using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ioget
{

    public interface Conversion
    {
        bool IsApplicable(ParameterInfo info);
        object Apply(string value);
    }


    public class StringConversion : Conversion
    {
        public bool IsApplicable(ParameterInfo value)
        {
            return true;
        }

        public object Apply(string value)
        {
            return value;
        }
    }

    public class Instantiator
    {
        private readonly List<Conversion> convertions; 
        public Instantiator()
        {
            this.convertions = new List<Conversion>
            {
                new IntConvertion(), new StringConversion()
            };
        }

        public object Bind(Dictionary<string, string> dic, Type type)
        {
            var constructor = type.GetConstructors().First();
            var parameters = constructor.GetParameters().Select(p => ResolveType(p, dic[p.Name]));
            return constructor.Invoke(parameters.ToArray());
        }

        private object ResolveType(ParameterInfo info, string value)
        {
            return convertions.First(c => c.IsApplicable(info)).Apply(value);
        }
    }

    public class IntConvertion : Conversion
    {

        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(int);
        }

        public object Apply(string value)
        {
            return Convert.ToInt32(value);
        }
    }
}
