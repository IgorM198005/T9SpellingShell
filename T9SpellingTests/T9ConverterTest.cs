using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling;
using System.IO;

namespace T9SpellingTests
{
    [TestClass]
    public class T9ConverterTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNonN()
        {
            using (MemoryStream input = new MemoryStream())
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);
                }
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNonIntegerN()
        {
            byte[] bytes;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("xa");                    
                }

                input.Close();

                bytes = input.ToArray();
            }

            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);
                }
            }            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNegativeN()
        {
            byte[] bytes;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("-10");
                }

                input.Close();

                bytes = input.ToArray();
            }

            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestZeroN()
        {
            byte[] bytes;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("0");
                }

                input.Close();

                bytes = input.ToArray();
            }

            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestHugeN()
        {
            byte[] bytes;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("101");
                }

                input.Close();

                bytes = input.ToArray();
            }

            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLessThenN()
        {
            byte[] bytes;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("2");
                    sw.WriteLine("hi");
                }

                input.Close();

                bytes = input.ToArray();
            }

            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);
                }
            }
        }

        [TestMethod]        
        public void TestValidN()
        {
            byte[] bytes;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("2");
                    sw.WriteLine("hi1");
                    sw.WriteLine("hi2");
                }

                input.Close();

                bytes = input.ToArray();
            }

            byte[] results;

            using (MemoryStream input = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine("Case #1: hi1");
                    sw.WriteLine("Case #2: hi2");
                }

                input.Close();

                results = input.ToArray();
            }

            using (MemoryStream input = new MemoryStream(bytes))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    new T9Converter(new FakeT9Translator()).Convert(input, output);

                    output.Close();

                    CollectionAssert.AreEquivalent(output.ToArray(), results);
                }
            }
        }
    }   
}
