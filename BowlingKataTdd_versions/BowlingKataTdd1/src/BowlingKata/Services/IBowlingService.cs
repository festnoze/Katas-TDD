namespace BowlingKata.Services;

public interface IBowlingService
{
    int FinalScore();
    void NewGame();
    void Roll(int pinsDownCount);
}