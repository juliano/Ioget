using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ioget
{
    public interface Conversion<out T>
    {
        bool IsApplicable(ParameterInfo info);

        object Apply(ParameterInfo info, string value);
    }

    public abstract class ApplicableConversion<T> : Conversion<T> where T : class
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(T);
        }

        public abstract object Apply(ParameterInfo info, string value);
    }

    public abstract class PrimitiveApplicableConversion<T> : Conversion<object> where T : struct 
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(T);
        }

        public abstract object Apply(ParameterInfo info, string value);
    }

    public class StringConversion : ApplicableConversion<string>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return value;
        }
    }

    public class LongConvertion : PrimitiveApplicableConversion<long>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToInt64(value);
        }
    }

    public class IntConvertion : PrimitiveApplicableConversion<int>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToInt32(value);
        }
    }

    public class EnumConvertion : Conversion<Enum>
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
