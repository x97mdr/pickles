namespace Gherkin3.Ast
{
    public class Step : IHasLocation
    {
        public Location Location { get; private set; }
        public string Keyword { get; private set; }
        public string Text { get; private set; }
        public StepArgument Argument { get; private set; }

        public Step(Location location, string keyword, string text, StepArgument argument)
        {
            this.Location = location;
            this.Keyword = keyword;
            this.Text = text;
            this.Argument = argument;
        }
    }
}