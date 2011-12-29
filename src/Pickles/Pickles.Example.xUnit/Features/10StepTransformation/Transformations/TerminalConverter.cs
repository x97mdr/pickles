using Specs.StepTransformation.Entities;
using TechTalk.SpecFlow;

namespace Specs.StepTransformation.Transformations
{
    [Binding]
    public class TerminalConverter
    {
        [StepArgumentTransformation("terminal (.*)")]
        public Terminal Transform(string terminalId)
        {
            return new Terminal { Id = terminalId };
        }
    }
}