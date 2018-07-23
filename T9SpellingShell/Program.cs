using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T9Spelling;
using System.IO;

namespace T9SpellingShell
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var input = File.OpenRead(args[0]))
            {
                using (var output = File.Create(args[1]))
                {
                    new T9Converter(T9Translator.Instance).Convert(input, output);
                }
            }
        }
    }
}
