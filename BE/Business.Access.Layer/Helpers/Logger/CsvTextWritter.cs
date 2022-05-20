using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Helpers.Logger
{
    public class CsvTextWritter : TextWriter
    {
        private readonly TextWriter _textWriter;

        public CsvTextWritter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public override Encoding Encoding => _textWriter.Encoding;

        public override void Write(char value)
        {
            _textWriter.Write(value);
            if (value == '"')
                _textWriter.Write(value);
        }

        public void WriteQuote()
        {
            _textWriter.Write('"');
        }
    }
}
