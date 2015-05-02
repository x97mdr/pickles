using System.Collections.Generic;

namespace Gherkin3.Ast
{
    public interface IHasSteps
    {
        IEnumerable<Step> Steps { get; }
    }
}