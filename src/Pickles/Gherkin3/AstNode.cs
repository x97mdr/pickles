using System.Collections.Generic;
using System.Linq;

namespace Gherkin3
{
    public class AstNode
    {
        private readonly Dictionary<RuleType, IList<object>> subItems = new Dictionary<RuleType, IList<object>>();
        public RuleType RuleType { get; private set; }

        public AstNode(RuleType ruleType)
        {
            this.RuleType = ruleType;
        }

        public Token GetToken(TokenType tokenType)
        {
            return this.GetSingle<Token>((RuleType)tokenType);
        }

        public IEnumerable<Token> GetTokens(TokenType tokenType)
        {
            return this.GetItems<Token>((RuleType)tokenType);
        }

        public T GetSingle<T>(RuleType ruleType)
        {
            return this.GetItems<T>(ruleType).SingleOrDefault();
        }

        public IEnumerable<T> GetItems<T>(RuleType ruleType)
        {
            IList<object> items;
            if (!this.subItems.TryGetValue(ruleType, out items))
            {
                return Enumerable.Empty<T>();
            }
            return items.Cast<T>();
        }

        public void SetSingle<T>(RuleType ruleType, T value)
        {
            this.subItems[ruleType] = new object[] { value };
        }

        public void AddRange<T>(RuleType ruleType, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                this.Add(ruleType, value);
            }
        }

        public void Add<T>(RuleType ruleType, T obj)
        {
            IList<object> items;
            if (!this.subItems.TryGetValue(ruleType, out items))
            {
                items = new List<object>();
                this.subItems.Add(ruleType, items);
            }
            items.Add(obj);
        }
    }
}
