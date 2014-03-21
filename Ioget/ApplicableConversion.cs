using System;
using System.Reflection;

namespace Ioget
{
    public interface Conversion<out T>
    {
        bool IsApplicable(ParameterInfo info);

        object Apply(string value);
    }

    public abstract class ApplicableConversion<T> : Conversion<T> where T : class
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(T);
        }

        public abstract object Apply(string value);
    }

    public abstract class PrimitiveApplicableConversion<T> : Conversion<object> where T : struct 
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(T);
        }

        public abstract object Apply(string value);
    }

    public class StringConversion : ApplicableConversion<string>
    {
        public override object Apply(string value)
        {
            return value;
        }
    }

    public class IntConvertion : PrimitiveApplicableConversion<int>
    {
        public override object Apply(string value)
        {
            return Convert.ToInt32(value);
        }
    }
}
