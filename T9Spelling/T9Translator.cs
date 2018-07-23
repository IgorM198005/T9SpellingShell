using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T9Spelling
{
    public sealed class T9Translator : IT9Translator
    {
        private readonly T9KeyRoute _spaceRoute;

        private readonly T9KeyRoute[] _t9KeyRoutes;

        private T9Translator()
        {
            _spaceRoute = new T9KeyRoute { KeyCode = '0', Repeat = 1 };

            _t9KeyRoutes = GetT9KeyRoutes();
        }

        public string Translate(string message)
        {
            if (message == null) return null;

            var sb = new StringBuilder(message.Length * 4);

            char lastKeyCode = '\0';

            foreach(var letter in message)
            {
                var route = Translate(letter);

                if (route.KeyCode == lastKeyCode) sb.Append(' ');

                sb.Append(route.KeyCode, route.Repeat);

                lastKeyCode = route.KeyCode;
            }

            return sb.ToString();
        }

        private static T9KeyRoute[] GetT9KeyRoutes()
        {
            char[][] layout = {
                new char[] { 'a','b','c' },
                new char[] { 'd','e','f' },
                new char[] { 'g','h','i' },
                new char[] { 'j','k','l' },
                new char[] { 'm','n','o' },
                new char[] { 'p','q','r','s' },
                new char[] { 't','u','v' },
                new char[] { 'w','x','y','z' }
            };

            var t9KeyRoutes = new T9KeyRoute['z' - 'a' + 1];

            char keyCode = '2';

            foreach(var subSet in layout)
            {
                int repeat = 1;
                foreach(var letter in subSet)
                {
                    t9KeyRoutes[letter - 'a'] = new T9KeyRoute
                    {
                        KeyCode = keyCode,
                        Repeat = repeat++
                    };
                }
                keyCode++;
            }

            return t9KeyRoutes;
        }

        private T9KeyRoute Translate(char letter)
        {
            if (letter == ' ')
                return _spaceRoute;
            else if (letter >= 'a' && letter <= 'z')
                return _t9KeyRoutes[letter - 'a'];

            throw new ArgumentOutOfRangeException("Message", 
                Properties.Resources.MessageLimitExceptionMessage);                
        }

        public static readonly T9Translator Instance = new T9Translator();
    }
}
