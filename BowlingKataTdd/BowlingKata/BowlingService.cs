using BowlingKata.Data;

namespace BowlingKata.Services;

public class BowlingService : IBowlingService
{
    public BowlingService()
    {
        bowlingScoreSheet = new BowlingGameScores();
    }

    private BowlingGameScores bowlingScoreSheet = new BowlingGameScores();
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
