using System;
using System.Reflection;

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

    public class LongConversion : PrimitiveApplicableConversion<long>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToInt64(value);
        }
    }

    public class IntConversion : PrimitiveApplicableConversion<int>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToInt32(value);
        }
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

    public class BooleanConversion : PrimitiveApplicableConversion<bool>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToBoolean(value);
        }
    }

    public class CharConversion : PrimitiveApplicableConversion<char>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToChar(value);
        }
    }

    public class ShortConversion : PrimitiveApplicableConversion<short>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToInt16(value);
        }
    }

    public class ConversionNotFound : Conversion<object>
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return true;
        }

        public object Apply(ParameterInfo info, string value)
        {
            throw new TypeConversionFailure("Could not find conversion to type " + info.ParameterType);
        }
    }

    public class TypeConversionFailure : Exception
    {
        public TypeConversionFailure(string message) : base(message)
        {
        }
    }
}
