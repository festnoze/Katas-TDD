using BowlingKata.Data;
using BowlingKata.Services;

[Binding]
public class BowlingSteps
{
    [Given(@"je commence une nouvelle partie de bowling")]
    public void GivenJeCommenceUneNouvellePartieDeBowling()
    {
        _bowlingService.NewGame();
        _currentRollCount = 0;
        _currentStrikeCount = 0;
    }

    [When(@"je renverse (.*) quilles")]
    public void WhenIRollPinsDown(int pinsCount)
    {
        _bowlingService.Roll(pinsCount);
        _currentRollCount++;
    }

    [When(@"je renverse (.*) quilles pour chacun des (.*) lancers")]
    public void WhenJeRenverseQuillesPourChacunDesLancers(int pinsCount, int rollsCount)
    {
        try
        {
            for (int i = 0; i < rollsCount; i++)
            {
                WhenIRollPinsDown(pinsCount);
            }
            _failure = false;
        }
        catch
        {
            _failure = true;
        }
    }

    [When(@"je fais un strike")]
    public void WhenIRollAStrike()
    {
        WhenIRollPinsDown(10);
        _currentStrikeCount++;
    }

    [When(@"je ne renverse aucunes quilles lors des lancers suivants")]
    public void WhenIRollNoPinsForTheRemainingThrows()
    {
        int remainingRolls = 20 - _currentRollCount - _currentStrikeCount; // 10 frames x 2 rolls per frame
        WhenJeRenverseQuillesPourChacunDesLancers(0, remainingRolls);
    }

    [When(@"je joue les lancers suivants (.*)")]
    public void WhenIPlayTheFollowingThrows(string rollResult)
    {
        var rollResults = rollResult.Split(',').Select(x => Convert.ToInt32(x.Trim()));
        foreach (int pins in rollResults)
        {
            _bowlingService.Roll(pins);
        }
    }

    [Then(@"le score final devrait être (.*)")]
    public void ThenTheFinalScoreShouldBe(int expectedScore)
    {
        _score = _bowlingService.FinalScore();
        Assert.Equal(expectedScore, _score);
    }

    [Then(@"le calcul du score final fini en échec: ([^""]*)")]
    public void ThenLeCalculDuScoreFinalFiniEnEchec(string awaitedFailure)
    {
        var awaitedFailureBool = awaitedFailure.Equals("oui") ? true : false;
        Assert.Equal(awaitedFailureBool, _failure);
    }

    #region private props & ctor

    private IBowlingService _bowlingService
    {
        get => _scenarioContext.Get<IBowlingService>("BowlingService");
        set => _scenarioContext.Set(value, "BowlingService");
    }
    private int _score
    {
        get => _scenarioContext.Get<int>("_score");
        set => _scenarioContext.Set(value, "_score");
    }
    private int _currentRollCount
    {
        get => _scenarioContext.Get<int>("_currentRollCount");
        set => _scenarioContext.Set(value, "_currentRollCount");
    }
    private int _currentStrikeCount
    {
        get => _scenarioContext.Get<int>("_currentStrikeCount");
        set => _scenarioContext.Set(value, "_currentStrikeCount");
    }
    private bool _failure
    {
        get => _scenarioContext.Get<bool>("_failure");
        set => _scenarioContext.Set(value, "_failure");
    }

    private readonly ScenarioContext _scenarioContext;

    public BowlingSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _bowlingService = new BowlingService();
    }
    #endregion
}
