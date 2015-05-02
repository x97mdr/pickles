using System.IO;
using Gherkin3.Ast;

namespace Gherkin3
{
    public class Parser : Parser<Feature>
    {
        public Feature Parse(TextReader reader)
        {
            return Parse(new TokenScanner(reader));
        }

        public Feature Parse(string sourceFile)
        {
            using (var reader = new StreamReader(sourceFile))
            {
                return Parse(new TokenScanner(reader));
            }
        }
    }
}
