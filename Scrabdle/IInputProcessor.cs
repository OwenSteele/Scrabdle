using Scrabdle.Entities;

namespace Scrabdle
{
    public interface IInputProcessor
    {
        InputResult<string> ProcessGuess(string guess);
    }
}