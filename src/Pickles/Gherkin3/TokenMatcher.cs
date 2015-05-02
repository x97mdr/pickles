using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gherkin3.Ast;

namespace Gherkin3
{
    public class TokenMatcher : ITokenMatcher
    {
		private readonly Regex LANGUAGE_PATTERN = new Regex ("^\\s*#\\s*language\\s*:\\s*([a-zA-Z\\-_]+)\\s*$");
        private readonly IGherkinDialectProvider dialectProvider;
        private GherkinDialect currentDialect;
        private string activeDocStringSeparator = null;
        private int indentToRemove = 0;

        public GherkinDialect CurrentDialect
        {
            get
            {
                if (this.currentDialect == null)
                    this.currentDialect = this.dialectProvider.DefaultDialect;
                return this.currentDialect;
            }
        }

        public TokenMatcher(IGherkinDialectProvider dialectProvider = null)
        {
            this.dialectProvider = dialectProvider ?? new GherkinDialectProvider();
        }

        protected virtual void SetTokenMatched(Token token, TokenType matchedType, string text = null, string keyword = null, int? indent = null, GherkinLineSpan[] items = null)
        {
            token.MatchedType = matchedType;
            token.MatchedKeyword = keyword;
            token.MatchedText = text;
            token.MatchedItems = items;
            token.MatchedGherkinDialect = this.CurrentDialect;
            token.MatchedIndent = indent ?? (token.Line == null ? 0 : token.Line.Indent);
            token.Location = new Location(token.Location.Line, token.MatchedIndent + 1);
        }

        public bool Match_EOF(Token token)
        {
            if (token.IsEOF)
            {
                this.SetTokenMatched(token, TokenType.EOF);
                return true;
            }
            return false;
        }

        public bool Match_Other(Token token)
        {
            var text = token.Line.GetLineText(this.indentToRemove); //take the entire line, except removing DocString indents
            this.SetTokenMatched(token, TokenType.Other, text, indent: 0);
            return true;
        }

        public bool Match_Empty(Token token)
        {
            if (token.Line.IsEmpty())
            {
                this.SetTokenMatched(token, TokenType.Empty);
                return true;
            }
            return false;
        }

        public bool Match_Comment(Token token)
        {
            if (token.Line.StartsWith(GherkinLanguageConstants.COMMENT_PREFIX))
            {
                var text = token.Line.GetLineText(); //take the entire line
                this.SetTokenMatched(token, TokenType.Comment, text, indent: 0);
                return true;
            }
            return false;
        }

        private ParserException CreateTokenMatcherException(Token token, string message)
        {
            return new AstBuilderException(message, new Location(token.Location.Line, token.Line.Indent + 1));
        }

        public bool Match_Language(Token token)
        {
            var match = this.LANGUAGE_PATTERN.Match(token.Line.GetLineText());

            if (match.Success)
            {
                var language = match.Groups[1].Value;
                this.SetTokenMatched(token, TokenType.Language, language);

                try
                {
                    this.currentDialect = this.dialectProvider.GetDialect(language, token.Location);
                }
                catch (NotSupportedException ex)
                {
                    throw this.CreateTokenMatcherException(token, ex.Message);
                }

                return true;
            }
            return false;
        }

        public bool Match_TagLine(Token token)
        {
            if (token.Line.StartsWith(GherkinLanguageConstants.TAG_PREFIX))
            {
                this.SetTokenMatched(token, TokenType.TagLine, items: token.Line.GetTags().ToArray());
                return true;
            }
            return false;
        }

        public bool Match_FeatureLine(Token token)
        {
            return this.MatchTitleLine(token, TokenType.FeatureLine, this.CurrentDialect.FeatureKeywords);
        }

        public bool Match_BackgroundLine(Token token)
        {
            return this.MatchTitleLine(token, TokenType.BackgroundLine, this.CurrentDialect.BackgroundKeywords);
        }

        public bool Match_ScenarioLine(Token token)
        {
            return this.MatchTitleLine(token, TokenType.ScenarioLine, this.CurrentDialect.ScenarioKeywords);
        }

        public bool Match_ScenarioOutlineLine(Token token)
        {
            return this.MatchTitleLine(token, TokenType.ScenarioOutlineLine, this.CurrentDialect.ScenarioOutlineKeywords);
        }

        public bool Match_ExamplesLine(Token token)
        {
            return this.MatchTitleLine(token, TokenType.ExamplesLine, this.CurrentDialect.ExamplesKeywords);
        }

        private bool MatchTitleLine(Token token, TokenType tokenType, string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                if (token.Line.StartsWithTitleKeyword(keyword))
                {
                    var title = token.Line.GetRestTrimmed(keyword.Length + GherkinLanguageConstants.TITLE_KEYWORD_SEPARATOR.Length);
                    this.SetTokenMatched(token, tokenType, keyword: keyword, text: title);
                    return true;
                }
            }
            return false;
        }

        public bool Match_DocStringSeparator(Token token)
        {
            return this.activeDocStringSeparator == null 
                // open
                ? this.Match_DocStringSeparator(token, GherkinLanguageConstants.DOCSTRING_SEPARATOR, true) ||
                  this.Match_DocStringSeparator(token, GherkinLanguageConstants.DOCSTRING_ALTERNATIVE_SEPARATOR, true)
                // close
                : this.Match_DocStringSeparator(token, this.activeDocStringSeparator, false);
        }

        private bool Match_DocStringSeparator(Token token, string separator, bool isOpen)
        {
            if (token.Line.StartsWith(separator))
            {
                string contentType = null;
                if (isOpen)
                {
                    contentType = token.Line.GetRestTrimmed(separator.Length);
                    this.activeDocStringSeparator = separator;
                    this.indentToRemove = token.Line.Indent;
                }
                else
                {
                    this.activeDocStringSeparator = null;
                    this.indentToRemove = 0;
                }

                this.SetTokenMatched(token, TokenType.DocStringSeparator, contentType);
                return true;
            }
            return false;
        }


        public bool Match_StepLine(Token token)
        {
            var keywords = this.CurrentDialect.StepKeywords;
            foreach (var keyword in keywords)
            {
                if (token.Line.StartsWith(keyword))
                {
                    var stepText = token.Line.GetRestTrimmed(keyword.Length);
                    this.SetTokenMatched(token, TokenType.StepLine, keyword: keyword, text: stepText);
                    return true;
                }
            }
            return false;
        }

        public bool Match_TableRow(Token token)
        {
            if (token.Line.StartsWith(GherkinLanguageConstants.TABLE_CELL_SEPARATOR))
            {
                this.SetTokenMatched(token, TokenType.TableRow, items: token.Line.GetTableCells().ToArray());
                return true;
            }
            return false;
        }
    }
}