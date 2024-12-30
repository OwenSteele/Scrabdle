using Microsoft.Extensions.DependencyInjection;
using Scrabdle.Entities;

namespace Scrabdle.Validation
{
    public interface IGuessValidator
    {
        InputResult<string> CheckGuess(string guess);
    }
}