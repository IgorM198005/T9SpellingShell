using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T9Spelling;

namespace T9SpellingTests
{
    public class FakeT9Translator : IT9Translator
    {
        public string Translate(string message) => message;
    }
}
