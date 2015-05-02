using System.Collections.Generic;

namespace Gherkin3.Ast
{
    public class Feature : IHasLocation, IHasDescription, IHasTags
    {
        public IEnumerable<Tag> Tags { get; private set; }
        public Location Location { get; private set; }
        public string Language { get; private set; }
        public string Keyword { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Background Background { get; private set; }
        public IEnumerable<ScenarioDefinition> ScenarioDefinitions { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }

        public Feature(Tag[] tags, Location location, string language, string keyword, string name, string description, Background background, ScenarioDefinition[] scenarioDefinitions, Comment[] comments)
        {
            this.Tags = tags;
            this.Location = location;
            this.Language = language;
            this.Keyword = keyword;
            this.Name = name;
            this.Description = description;
            this.Background = background;
            this.ScenarioDefinitions = scenarioDefinitions;
            this.Comments = comments;
        }
    }
}
