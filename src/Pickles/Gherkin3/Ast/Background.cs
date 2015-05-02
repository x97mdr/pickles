using System.Collections.Generic;

namespace Gherkin3.Ast
{
    public class Background : IHasLocation, IHasDescription, IHasSteps
    {
        public Location Location { get; private set; }
        public string Keyword { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Step> Steps { get; private set; }

        public Background(Location location, string keyword, string name, string description, Step[] steps)
        {
            this.Location = location;
            this.Keyword = keyword;
            this.Name = name;
            this.Description = description;
            this.Steps = steps;
        }
    }
}