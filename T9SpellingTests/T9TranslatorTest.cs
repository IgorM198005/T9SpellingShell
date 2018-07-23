using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling;

namespace T9SpellingTests
{
    [TestClass]
    public class T9TranslatorTest
    {
        [TestMethod]
        public void TestHelloWorld()
        {
            Assert.AreEqual(T9Translator.Instance.Translate("hello world"), "4433555 555666096667775553");            
        }

        [TestMethod]
        public void TestFooBar()
        {
            Assert.AreEqual(T9Translator.Instance.Translate("foo  bar"), "333666 6660 022 2777");
        }

        [TestMethod]
        public void TestYes()
        {
            Assert.AreEqual(T9Translator.Instance.Translate("yes"), "999337777");
        }

        [TestMethod]
        public void TestHi()
        {
            Assert.AreEqual(T9Translator.Instance.Translate("hi"), "44 444");
        }

        [TestMethod]
        public void TestNull()
        {
            Assert.AreEqual(T9Translator.Instance.Translate(null), null);
        }

        [TestMethod]
        public void TestEmpty()
        {
            Assert.AreEqual(T9Translator.Instance.Translate(string.Empty), string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalid()
        {
            Assert.AreEqual(T9Translator.Instance.Translate("---?"), "---?");
        }
    }
}
