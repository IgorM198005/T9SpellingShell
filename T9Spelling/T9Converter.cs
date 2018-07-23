using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace T9Spelling
{
    public class T9Converter
    {
        private IT9Translator _translator;

        public T9Converter(IT9Translator translator)
        {
            _translator = translator ?? throw new ArgumentNullException(nameof(translator));
        }

        public void Convert(Stream input, Stream output)
        {
            using (var sr = new StreamReader(input))
            {
                var line = sr.ReadLine();
                
                if (string.IsNullOrEmpty(line)) throw new ArgumentException(Properties.Resources.NoIntegerNatFirstLineMessage);

                if (!int.TryParse(line, NumberStyles.None, CultureInfo.InvariantCulture, out var n))
                {
                    throw new ArgumentException(Properties.Resources.NoIntegerNatFirstLineMessage);
                }

                if (n < Limits.MinValueOfN || n > Limits.MaxValueOfN)
                {
                    throw new ArgumentOutOfRangeException("N", 
                        string.Format(
                            CultureInfo.InvariantCulture, 
                            Properties.Resources.NOutOfRangeMessage,
                            Limits.MinValueOfN,
                            Limits.MaxValueOfN));
                }

                using(StreamWriter sw = new StreamWriter(output))
                {
                    for (int i = 1; i <= n; i++)
                    {
                        if ((line = sr.ReadLine()) == null)
                        {
                            throw new ArgumentException(
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    Properties.Resources.LinesLessThenNMessage, n));
                        }

                        if (line.Length < Limits.MinValueOfMessageLength 
                            || line.Length > Limits.MaxValueOfMessageLength)
                        {
                            throw new ArgumentOutOfRangeException("Message Length",
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    Properties.Resources.MessageLengthExceptionMessage,
                                    Limits.MinValueOfMessageLength,
                                    Limits.MaxValueOfMessageLength));
                        }

                        sw.WriteLine(FormattableString.Invariant($"Case #{i}: {_translator.Translate(line)}"));
                    }
                }                
            }
        }
    }
}
