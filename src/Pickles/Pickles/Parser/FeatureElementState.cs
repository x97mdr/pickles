using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public struct FeatureElementState
    {
        public bool IsBackgroundActive;
        public bool IsScenarioActive;
        public bool IsScenarioOutlineActive;

        public void SetBackgroundActive()
        {
            IsBackgroundActive = true;
            IsScenarioActive = false;
            IsScenarioOutlineActive = false;
        }

        public void SetScenarioActive()
        {
            IsBackgroundActive = false;
            IsScenarioActive = true;
            IsScenarioOutlineActive = false;
        }

        public void SetScenarioOutlineActive()
        {
            IsBackgroundActive = false;
            IsScenarioActive = false;
            IsScenarioOutlineActive = true;
        }
    }
}
