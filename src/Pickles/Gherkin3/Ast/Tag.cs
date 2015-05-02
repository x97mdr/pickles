namespace Gherkin3.Ast
{
    public class Tag : IHasLocation
    {
        public Location Location { get; private set; }
        public string Name { get; private set; }

        public Tag(Location location, string name)
        {
            this.Name = name;
            this.Location = location;
        }
    }
}