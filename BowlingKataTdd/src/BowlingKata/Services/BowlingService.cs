using BowlingKata.Data;

namespace BowlingKata.Services;

public class BowlingService : IBowlingService
{
    // WARNING: this is not a state-less version as the service hold the internal bowlingScoreSheet status, if services are accessed through webservices and concurrency apply
    // If its the case, rather move to a state-less service, and save and retrieve the bowlingScoreSheet state from a repository
    private BowlingGameScores bowlingScoreSheet = new BowlingGameScores();

    public BowlingService()
    {
        bowlingScoreSheet = new BowlingGameScores();
    }

    public void NewGame()
    {
        bowlingScoreSheet = new BowlingGameScores();
    }

    public void Roll(int pinsDownCount)
    {
        bowlingScoreSheet.AddRollScore(pinsDownCount);
    }

    public int FinalScore()
    {
        return bowlingScoreSheet.FinalScore();
    }
}
