using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling;
using System.IO;

namespace T9SpellingTests
{
    [TestClass]
    public class T9IntegratedTest
    {
        [TestMethod]
        public void TestLargeDataset()
        {
            using (var input = new MemoryStream(Properties.Resources.C_large_practice_in))
            {
                using (var output = new MemoryStream())
                {
                    new T9Converter(T9Translator.Instance).Convert(input, output);

                    output.Close();

                    CollectionAssert.AreEquivalent(Properties.Resources.C_large_practice_out, output.ToArray());
                }                    
            }
            
        }

        [TestMethod]
        public void TestSmallDataset()
        {
            using (var input = new MemoryStream(Properties.Resources.C_small_practice_in))
            {
                using (var output = new MemoryStream())
                {
                    new T9Converter(T9Translator.Instance).Convert(input, output);

                    output.Close();

                    CollectionAssert.AreEquivalent(Properties.Resources.C_small_practice_out, output.ToArray());
                }
            }
        }
    }
}
