using System;
using TechTalk.SpecFlow;

namespace Specs.StepTransformation.Transformations
{
    [Binding]
    public class DateConverter
    {
        [StepArgumentTransformation("date (.*)")]
        public DateTime Transform(string dateString)
        {
            return DateTime.Parse(dateString);
        }
    }
}