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
}