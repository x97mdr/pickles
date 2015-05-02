using Gherkin3.Ast;

namespace Gherkin3
{
    public class Token
    {
        public bool IsEOF { get { return this.Line == null; } }
        public IGherkinLine Line { get; set; }
        public TokenType MatchedType { get; set; }
        public string MatchedKeyword { get; set; }
        public string MatchedText { get; set; }
        public GherkinLineSpan[] MatchedItems { get; set; }
        public int MatchedIndent { get; set; }
        public GherkinDialect MatchedGherkinDialect { get; set; }
        public Location Location { get; set; }

        public Token(IGherkinLine line, Location location)
        {
            this.Line = line;
            this.Location = location;
        }

        public void Detach()
        {
            if (this.Line != null)
                this.Line.Detach();
        }

        public string GetTokenValue()
        {
            return this.IsEOF ? "EOF" : this.Line.GetLineText(-1);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}/{2}", this.MatchedType, this.MatchedKeyword, this.MatchedText);
        }
    }
}