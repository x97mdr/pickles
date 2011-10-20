using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gherkin.lexer;

namespace Pickles.Parser
{
    public class PicklesListener : Listener
    {
        private readonly Feature theFeature;

        public PicklesListener()
        {
            theFeature = new Feature();
        }

        public Feature GetFeature()
        {
            return this.theFeature;
        }

        #region Listener Members

        public void background(string keyword, string name, string description, int line)
        {
            throw new NotImplementedException();
        }

        public void comment(string comment, int line)
        {
            throw new NotImplementedException();
        }

        public void eof()
        {
            throw new NotImplementedException();
        }

        public void examples(string keyword, string name, string description, int line)
        {
            throw new NotImplementedException();
        }

        public void feature(string keyword, string name, string description, int line)
        {
            this.theFeature.Name = name;
            this.theFeature.Description = description;
        }

        public void pyString(string pyString, int line)
        {
            throw new NotImplementedException();
        }

        public void row(java.util.List cells, int line)
        {
            throw new NotImplementedException();
        }

        public void scenario(string keyword, string name, string description, int line)
        {
            throw new NotImplementedException();
        }

        public void scenarioOutline(string keyword, string name, string description, int line)
        {
            throw new NotImplementedException();
        }

        public void step(string keyword, string name, int line)
        {
            throw new NotImplementedException();
        }

        public void tag(string tag, int line)
        {
            throw new NotImplementedException();
        }

        public void docString(string contentType, string content, int line)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
