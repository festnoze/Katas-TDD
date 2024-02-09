using KataTddBowling.Appli;
using KataTddBowling.Infra;
using System;
using TechTalk.SpecFlow;

namespace KataTddBowling.Tests
{
    [Binding]
    public class CalculDuScoreDuBowlingStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        public CalculDuScoreDuBowlingStepDefinitions(ScenarioContext context)
        {
            this.scenarioContext = context;
            service = new BowlingService(new FakeBowlingGameRepository());
            gameId = service.CreateNewGame();
        }

        [Given(@"on a fait les lancers suivants : (.*)")]
        public void GivenOnAFaitLesLancersSuivants(string rollsValuesAsString)
        {
            rollsValues = GetValuesFromString(rollsValuesAsString);
            foreach (var roll in rollsValues) 
            {
                service.AddRoll(gameId, roll);
            }
        }

        [When(@"on calcul le score de la partie")]
        public void WhenOnCalculLeScoreDeLaPartie()
        {
            score = service.GetScore(gameId);
        }

        [Then(@"le score récupéré vaut l'addition de tous les lancers")]
        public void ThenLeScoreRecupereVautLadditionDeTousLesLancers()
        {
            var addedRolls = rollsValues.Sum();
            score.Should().Be(addedRolls);
        }

        [Then(@"le score récupéré vaut le score attendu : (.*)")]
        public void ThenLeScoreRecupereVautLeScoreAttendu(int awaitedScore)
        {
            score.Should().Be(awaitedScore);
        }

        private Guid gameId
        {
            get => scenarioContext.Get<Guid>("gameId");
            set => scenarioContext.Set(value, "gameId");
        }

        private int[] rollsValues
        {
            get => scenarioContext.Get<int[]>("rollsValues");
            set => scenarioContext.Set(value, "rollsValues");
        }

        private int[] GetValuesFromString(string rollsValuesAsString)
        {
            return rollsValuesAsString.Split(',', StringSplitOptions.TrimEntries)
                                        .Select(int.Parse)
                                        .ToArray();
        }

        private int score
        {
            get => scenarioContext.Get<int>("score");
            set => scenarioContext.Set(value, "score");
        }

        private BowlingService service
        {
            get => scenarioContext.Get<BowlingService>("service");
            set => scenarioContext.Set(value, "service");
        }
    }
}
