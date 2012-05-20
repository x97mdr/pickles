using System.Collections.Generic;
using System.Linq;
using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.Svenska
{
    [Binding]
    public class SvenskaSteg
    {
        private const string TALLISTA_NYCKEL = "TalLista";
        private const string SUMMA_NYCKEL = "Summa";

        private List<int> TalLista
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey(TALLISTA_NYCKEL))
                {
                    ScenarioContext.Current.Set(new List<int>(), TALLISTA_NYCKEL);
                }

                return ScenarioContext.Current.Get<List<int>>(TALLISTA_NYCKEL);
            }
        }


        [Given(@"att jag har knappat in (\d+)")]
        public void GivetAttJagHarKnappatInTal(int talAttKnappaIn)
        {
            TalLista.Add(talAttKnappaIn);
        }

        [When(@"jag summerar")]
        public void NarJagSummerar()
        {
            ScenarioContext.Current.Set(TalLista.Sum(), SUMMA_NYCKEL);
        }

        [Then(@"ska resultatet vara (\d+)")]
        public void SaSkaResultatetVara(int förväntatResultat)
        {
            int summa = int.Parse(ScenarioContext.Current[SUMMA_NYCKEL].ToString());
            summa.Should().Equal(förväntatResultat);
        }
    }
}