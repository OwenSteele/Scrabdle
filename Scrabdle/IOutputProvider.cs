using Scrabdle.Entities;

namespace Scrabdle
{
    public interface IOutputProvider
    {
        Queue<OutputSection> GetFinalScores();
        Queue<OutputSection> GetFormattedGuesses();
        string GetInitialMessage();
    }
}