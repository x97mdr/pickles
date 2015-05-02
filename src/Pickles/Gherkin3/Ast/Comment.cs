namespace Gherkin3.Ast
{
    public class Comment : IHasLocation
    {
        public Location Location { get; private set; }
        public string Text { get; private set; }

        public Comment(Location location, string text)
        {
            this.Text = text;
            this.Location = location;
        }
    }
}