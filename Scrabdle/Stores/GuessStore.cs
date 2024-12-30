using System.Linq;
using System.Text;

namespace Scrabdle.Stores
{
    public class GuessStore : IGuessStore
    {
        private int _guessCounter;
        private int _wordLength;
        private char[][] _guesses = [];
        private bool _initialised;

        public int WordLength => _wordLength;
        public int Guesses => _guessCounter;

        public void Init(int guessLimit, int wordLength, string startingWord)
        {
            if (_initialised)
            {
                throw new InvalidOperationException("Store already steup.");
            }
            else
            {
                _initialised = true;
            }

            var hasStartingWord = startingWord.Length == wordLength;

            guessLimit++;

            _wordLength = wordLength;

            _guesses = new char[guessLimit][];

            for (var i = 0; i < guessLimit; i++)
            {
                _guesses[i] = new char[_wordLength];
            }

            _guessCounter = 0;

            if (hasStartingWord)
            {
                _guesses[0] = startingWord.ToCharArray();
                _guessCounter++;
            }
        }

        public void AddGuess(string guess) => AddGuess(guess.ToCharArray());

        public void AddGuess(char[] guess)
        {
            if (guess.Length != _wordLength)
            {
                throw new InvalidOperationException("Word is invalid length.");
            }

            _guesses[_guessCounter] = guess;

            _guessCounter++;
        }

        public char[][] GetGuesses()
        {
            return _guesses;
        }

        public int GetGuessIndex(char[] letters)
        {
            for (int i = 0; i < _guesses.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (letters[j] != _guesses[i][j])
                    {
                        break;
                    }
                    else if (j == letters.Length - 1)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public string GetVerticalGuess(int i)
        {
            return string.Join(string.Empty, _guesses.Select(x => x[i]));
        }

        public string GetDiagonalGuess(int i, bool reverse)
        {
            var sb = new StringBuilder();

            var rMax = _guesses.Length - 1;
            var cMax = reverse ? 0 : _wordLength;
            var r = 0;
            var c = reverse ? _wordLength - 1 - i : i;
            var a = reverse ? -1 : 1;

            while (r <= rMax && c != cMax)
            {
                sb.Append(_guesses[r][c]);

                r++;
                c += a;
            }

            return sb.ToString();
        }

        public void Reset()
        {
            _initialised = false;
            _wordLength = 0;
            _guesses = [];
            _guessCounter = 0;
        }
    }
}
