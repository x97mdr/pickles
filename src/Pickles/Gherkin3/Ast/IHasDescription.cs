namespace Gherkin3.Ast
{
    public interface IHasDescription
    {
        string Keyword { get; }
        string Name { get; }
        string Description { get; }
    }
}