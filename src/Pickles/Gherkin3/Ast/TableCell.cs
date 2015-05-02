namespace Gherkin3.Ast
{
    public class TableCell : IHasLocation
    {
        public Location Location { get; private set; }
        public string Value { get; private set; }

        public TableCell(Location location, string value)
        {
            this.Location = location;
            this.Value = value;
        }
    }
}