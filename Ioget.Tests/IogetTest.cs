using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using System.Collections.Generic;

namespace Ioget.Tests
{
    [TestFixture]
    public class IogetTest
    {
        [Test]
        public void ShouldContructObjectWithParamenterString()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "a"},
            };

            var myObject = new Instantiator().Bind<My<string>>(dic);
            Assert.AreEqual(My.Create("a"), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterInt()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "1"}
            };

            var myObject = new Instantiator().Bind<My<int>>(dic);
            Assert.AreEqual(My.Create(1), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterLong()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "1"}
            };

            var myObject = new Instantiator().Bind<My<long>>(dic);
            Assert.AreEqual(My.Create(1L), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterEnumFromString()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "Asdrubal"}
            };

            var myObject = new Instantiator().Bind<My<E>>(dic);
            Assert.AreEqual(My.Create(E.Asdrubal), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterBoolean()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "true"}
            };

            var myObject = new Instantiator().Bind<My<bool>>(dic);
            Assert.AreEqual(My.Create(true), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterChar()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "a"}
            };

            var myObject = new Instantiator().Bind<My<char>>(dic);
            Assert.AreEqual(My.Create('a'), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterShort()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "123"}
            };

            var myObject = new Instantiator().Bind<My<short>>(dic);
            Assert.AreEqual(My.Create((Int16)123), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterByte()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "123"}
            };

            var myObject = new Instantiator().Bind<My<byte>>(dic);
            Assert.AreEqual(My.Create((byte)123), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterFloat()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "99.12345"}
            };

            var myObject = new Instantiator().Bind<My<float>>(dic);
            Assert.AreEqual(My.Create((float)99.12345), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterDouble()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "99.12345"}
            };

            var myObject = new Instantiator().Bind<My<double>>(dic);
            Assert.AreEqual(My.Create(99.12345), myObject);
        }

        [Test]
        public void ShouldRefuseToConstructIfNotAllParamentersAreSent()
        {
            var dic = new Dictionary<string, string>
            {
                {"xxxx", "99.12345"}
            };

            try
            {
                new Instantiator().Bind<My<double>>(dic);
                Assert.Fail();
            }
            catch (MissingParameterKey)
            {
            }
        }

        [Test]
        public void ShouldRefuseToConstructIfConstructorNotIsFound()
        {
            var dic = new Dictionary<string, string>();

            try
            {
                new Instantiator().Bind<My<double>>(dic);
                Assert.Fail();
            }
            catch (MissingConstructor)
            {
            }
        }

        [Test]
        public void ShouldContructObjectWithNonPrimitiveParameter()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1.f1", "asdf"}
            };

            var myObject = new Instantiator().Bind<My<My<string>>>(dic);
            Assert.AreEqual(My.Create(My.Create("asdf")), myObject);
        }

        [Test]
        public void ShouldWorkWithTuples()
        {
            var dic = new Dictionary<string, string>
            {
                {"item1", "a"}
            };

            var myObject = new Instantiator().Bind<Tuple<string>>(dic);
            Assert.AreEqual(new Tuple<string>("a"), myObject);
        }

        [Test]
        public void ShouldConstructObjectUsingSuitableConstructor()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "a"},
                {"f2", "1"}
            };

            var myObject = new Instantiator().Bind<TwoParams>(dic);
            Assert.AreEqual(new TwoParams("a", 1), myObject);
        }

        [Test]
        public void ShouldConstructObjectUsingCustomConversion()
        {
            var dic = new Dictionary<string, string>
            {
                {"enumeration", "2"}
            };

            var myObject = new Instantiator()
                .SetCustomConversion(new EnumerationConversion<CustomEnumeration>())
                .Bind<UseEnumerationParams>(dic);

            Assert.AreEqual(CustomEnumeration.No, myObject.Enumeration);
        }
    }

    public enum E
    {
        Asdrubal, Abobrinha
    }

    public struct My<T>
    {
        private readonly T f1;
        public My(T f1)
        {
            this.f1 = f1;
        }
    }
    public static class My
    {
        public static My<T> Create<T>(T t) { return new My<T>(t); }
    }

    public struct TwoParams
    {
        private readonly string f1;
        private readonly int f2;

        public TwoParams(string f1)
        {
            this.f1 = f1;
            this.f2 = 0;
        }

        public TwoParams(string f1, int f2)
        {
            this.f1 = f1;
            this.f2 = f2;
        }
    }

    public class UseEnumerationParams
    {
        private readonly CustomEnumeration enumeration;

        public UseEnumerationParams(CustomEnumeration enumeration)
        {
            this.enumeration = enumeration;
        }

        public CustomEnumeration Enumeration
        {
            get { return enumeration; }
        }
    }


    public sealed class EnumerationConversion<T> : Conversion<object> where T : Enumeration, new()
    {
        public bool IsApplicable(ParameterInfo info)
        {
            return info.ParameterType == typeof(T);
        }

        public object Apply(ParameterInfo info, string value)
        {
            return Enumeration.FromValue<T>(Convert.ToInt32(value));
        }
    }

    public class CustomEnumeration : Enumeration
    {
        public CustomEnumeration()
        {
        }

        public CustomEnumeration(int value, string displayName)
            : base(value, displayName)
        {
        }

        public static CustomEnumeration Yes = new CustomEnumeration(1, "Sim");
        public static CustomEnumeration No = new CustomEnumeration(2, "Não");
        public static CustomEnumeration Perhaps = new CustomEnumeration(3, "Talvez");
    }

    [Serializable]
    public abstract class Enumeration : IComparable, IComparable<Enumeration>, IEquatable<Enumeration>
    {
        private readonly int value;
        private readonly string displayName;

        protected Enumeration() { }

        protected Enumeration(int value, string displayName)
        {
            this.value = value;
            this.displayName = displayName;
        }

        public int Value
        {
            get { return value; }
        }

        public string DisplayName
        {
            get { return displayName; }
        }

        public bool Equals(Enumeration other)
        {
            if (other == null) return false;
            return GetType() == other.GetType() && value == other.Value && displayName == other.DisplayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public static int MaxValue<T>() where T : Enumeration, new()
        {
            return GetAll<T>().Max(x => x.Value);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return (from info in fields let instance = new T() select info.GetValue(instance)).OfType<T>();
        }

        public static IEnumerable<Enumeration> GetAll(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return from info in fields let instance = Activator.CreateInstance(type) select (Enumeration)info.GetValue(instance);
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null) return false;
            return Equals(otherValue);
        }

        public override int GetHashCode()
        {
            return (value + displayName).GetHashCode();
        }

        public static T FromValue<T>(int value) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public int CompareTo(Enumeration other)
        {
            return Value.CompareTo(other.Value);
        }

        public virtual int CompareTo(object other)
        {
            return CompareTo((Enumeration)other);
        }

        public static Enumeration FromValueOrDefault(Type enumerationType, int enumerationValue)
        {
            return GetAll(enumerationType).SingleOrDefault(e => e.Value == enumerationValue);
        }

        public static Enumeration FromDisplayNameOrDefault(Type enumerationType, string displayName)
        {
            return GetAll(enumerationType).SingleOrDefault(e => e.DisplayName == displayName);
        }

        public static explicit operator int(Enumeration enumeration)
        {
            return enumeration.Value;
        }

        public static explicit operator string(Enumeration enumeration)
        {
            return enumeration.DisplayName;
        }
    }
}