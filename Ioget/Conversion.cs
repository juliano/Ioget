using System;
using System.Reflection;

namespace Ioget
{
    public interface Conversion<out T>
    {
        bool IsApplicable(ParameterInfo info);

        object Apply(ParameterInfo info, string value);
    }

    public class EnumConversion : Conversion<Enum>
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType.IsEnum;
        }

        public object Apply(ParameterInfo info, string value)
        {
            return Enum.Parse(info.ParameterType, value);
        }
    }
}
