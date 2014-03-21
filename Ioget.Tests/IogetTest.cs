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

            var myObject = new Instantiator().Bind(dic, typeof(MyString));
            Assert.AreEqual(new MyString("a"), myObject);
        }

        [Test]
        public void ShouldContructObjectWithParameterInt()
        {
            var dic = new Dictionary<string, string>
            {
                {"f1", "1"}
            };

            var myObject = new Instantiator().Bind(dic, typeof(MyInt));
            Assert.AreEqual(new MyInt(1), myObject);
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

    public struct MyString
    {
        private readonly string f1;

        public MyString(string f1)
        {
            this.f1 = f1;
        }
    }

    public struct MyInt
    {
        private readonly int f1;

        public MyInt(int f1)
        {
            this.f1 = f1;
        }
    }
}