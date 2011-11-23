using Specs.StepTransformation.Entities;
using TechTalk.SpecFlow;

namespace Specs.StepTransformation.Transformations
{
    [Binding]
    public class UserConverter
    {
        [StepArgumentTransformation]
        public User Transform(string name)
        {
            return new User { Name = name };
        }
    }
}