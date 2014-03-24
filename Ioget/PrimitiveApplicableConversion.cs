using System;
using System.Globalization;
using System.Reflection;

namespace Ioget
{
    public abstract class PrimitiveApplicableConversion<T> : Conversion<object> where T : struct
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(T);
        }

        public abstract object Apply(ParameterInfo info, string value);
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

    public class ByteConversion : PrimitiveApplicableConversion<byte>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return Convert.ToByte(value);
        }
    }

    public class FloatConversion : PrimitiveApplicableConversion<float>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
    }

    public class DoubleConversion : PrimitiveApplicableConversion<double>
    {
        public override object Apply(ParameterInfo info, string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}
