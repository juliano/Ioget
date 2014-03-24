using System;
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

            var myObject = new Instantiator().Bind(dic, typeof(My<string>));
            Assert.AreEqual(My.Create("a"), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterInt()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "1"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<int>));
            Assert.AreEqual(My.Create(1), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterLong()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "1"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<long>));
            Assert.AreEqual(My.Create(1L), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterEnumFromString()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "Asdrubal"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<E>));
            Assert.AreEqual(My.Create(E.Asdrubal), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterBoolean()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "true"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<bool>));
            Assert.AreEqual(My.Create(true), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterChar()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "a"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<char>));
            Assert.AreEqual(My.Create('a'), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterShort()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "123"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<short>));
            Assert.AreEqual(My.Create((Int16)123), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterByte()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "123"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<byte>));
            Assert.AreEqual(My.Create((byte)123), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterFloat()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "99.12345"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<float>));
            Assert.AreEqual(My.Create((float)99.12345), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterDouble()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "99.12345"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(My<double>));
            Assert.AreEqual(My.Create(99.12345), myObject);
        }

        [Test]
        public void ShouldRefuseToConstructIfNotAllParamentersAreSent()
        {
            var dic = new Dictionary<string, string>();

            try
            {
                var myObject = new Instantiator().Bind(dic, typeof(My<double>));
                Assert.Fail();
            }
            catch (MissingParameterKey e)
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

            var myObject = new Instantiator().Bind(dic, typeof(My<My<string>>));
            Assert.AreEqual(My.Create(My.Create("asdf")), myObject);
        }

        [Test]
        public void ShouldWorkWithTuples()
        {
            var dic = new Dictionary<string, string>
            {
                {"item1", "a"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(Tuple<string>));
            Assert.AreEqual(new Tuple<string>("a"), myObject);
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

}