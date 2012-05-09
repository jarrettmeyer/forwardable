using System;
using System.Dynamic;
using NUnit.Framework;

namespace ForwardLib.Tests
{
    [TestFixture]
    public class Forwardable_Tests
    {
        private TestObject testObject;
        private dynamic testObjectPresenter;

        [SetUp]
        public void before_each_test()
        {
            testObject = new TestObject();
            testObjectPresenter = new TestObjectPresenter(testObject);
        }

        [Test]
        public void can_create_new_property()
        {
            Assert.AreEqual("0", testObjectPresenter.CastedIntValue);
        }

        [Test]
        public void can_get_nested_int_property_value()
        {
            testObject.IntValue = 9;
            Assert.AreEqual(9, testObjectPresenter.IntValue);
        }

        [Test]
        public void can_get_nested_string_property_value()
        {
            testObject.StringValue = "Hello";
            Assert.AreEqual("Hello", testObjectPresenter.StringValue);
        }
    }

    public class TestObject
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
    }

    public class TestObjectPresenter : Forwardable<TestObject>
    {
        public TestObjectPresenter(TestObject testObject)
            : base(testObject) { }

        public string CastedIntValue
        {
            get
            {
                dynamic obj = this;
                return obj.IntValue.ToString();
            }
        }
    }
}
