using Scrabdle.Entities;
using Scrabdle.Stores;
using Scrabdle.Validation;

namespace Scrabdle
{
    public class InputProcessor(
        IGuessStore guessStore,
        IGameStore gameStore,
        IGuessValidator guessValidator,
        IDictionaryProcessor dictionaryProcessor)
    : IInputProcessor
    {
        private readonly IGuessStore guessStore = guessStore;
        private readonly IGameStore gameStore = gameStore;
        private readonly IGuessValidator guessValidator = guessValidator;
        private readonly IDictionaryProcessor dictionaryProcessor = dictionaryProcessor;

        public InputResult<string> ProcessGuess(string guess)
        {
            InputResult<string> result;

            if (guessStore.Guesses < gameStore.GuessLimit)
            {
                guess = guess.Replace(" ", string.Empty).ToUpper();

                result = guessValidator.CheckGuess(guess);

                if (result.Valid)
                {
                    result = dictionaryProcessor.CheckGuess(guess);

                    if (result.Valid)
                    {
                        guessStore.AddGuess(guess);
                        gameStore.AddScore(guessStore.Guesses, guess.GetScores());
                    }
                }
            }
            else
            {
                result = new(guess, "No guesses remaining");
            }

            return result;
        }
    }
}
