using System.IO;
using Gherkin3.Ast;

namespace Gherkin3
{
    public class TokenScanner : ITokenScanner
    {
        protected int lineNumber = 0;
        protected readonly TextReader reader;

        public TokenScanner(TextReader reader)
        {
            this.reader = reader;
        }

        public virtual Token Read()
        {
            var line = this.reader.ReadLine();
            var location = new Location(++this.lineNumber);
            return line == null ? new Token(null, location) : new Token(new GherkinLine(line, this.lineNumber), location);
        }
    }
}