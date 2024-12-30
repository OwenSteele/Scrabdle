using Scrabdle.Entities;
using Scrabdle.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scrabdle.Validation
{
    public partial class GuessValidator(IGuessStore guessStore) : IGuessValidator
    {
        private readonly IGuessStore guessStore = guessStore;

        public InputResult<string> CheckGuess(string guess)
        {
            if (string.IsNullOrWhiteSpace(guess))
            {
                return new(guess, "No Guess Made");
            }
            else if (guess.Length != guessStore.WordLength)
            {
                return new(guess, $"Guess must be {guessStore.WordLength} characters");
            }
            else if (!MyRegex().IsMatch(guess))
            {
                return new(guess, "Guess can only contain letters");
            }
            else if (guessStore.GetGuessIndex(guess.ToCharArray()) >= 0)
            {
                return new(guess, "Word already used");
            }

            return new(guess);
        }

        [GeneratedRegex("[a-zA-Z]")]
        private static partial Regex MyRegex();
    }
}
