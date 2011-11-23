using System;

namespace Specs.TestEntities
{
    public class Person
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public DateTime BirthDate { get; set; }

        // Note: NO Cred value - it's simply skipped

        public override bool Equals(object obj)
        {
            var pIn = obj as Person;

            return (Name == pIn.Name) &&
                (Style == pIn.Style) &&
                (BirthDate == pIn.BirthDate);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}