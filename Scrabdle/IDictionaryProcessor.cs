using Scrabdle.Entities;

namespace Scrabdle
{
    public interface IDictionaryProcessor
    {
        InputResult<string> CheckGuess(string guess);
    }
}