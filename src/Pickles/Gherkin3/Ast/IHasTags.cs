using System.Collections.Generic;

namespace Gherkin3.Ast
{
    public interface IHasTags
    {
        IEnumerable<Tag> Tags { get; }
    }
}