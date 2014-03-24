using System.Reflection;

namespace Ioget
{
    public abstract class ApplicableConversion<T> : Conversion<T> where T : class
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
}
