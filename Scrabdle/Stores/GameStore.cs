namespace Scrabdle.Stores
{
    public class GameStore : IGameStore
    {
        private int _guessLimit;
        private int _verticalMulitplier;
        private int _diagonalMulitplier;
        private int[][] _scores = [];
        private bool _initialised;
        private string _startingWord = string.Empty;
        private bool _hasStartingWord = false;
        public int GuessLimit => _guessLimit;
        public int TotalGuessScore => _scores.Skip(1).Sum(x => x.Sum());

        public string StartingWord => _startingWord;

        public void Init(int guessLimit, int wordLength, int verticalMulitplier, int diagonalMulitplier, string startingWord)
        {
            if (_initialised)
            {
                throw new InvalidOperationException("Store already steup.");
            }
            else
            {
                _initialised = true;
            }

            _hasStartingWord = startingWord.Length == wordLength;

            guessLimit++;

            _guessLimit = guessLimit;
            _verticalMulitplier = verticalMulitplier;
            _diagonalMulitplier = diagonalMulitplier;

            _startingWord = startingWord;

            _scores = new int[guessLimit][];

            for (var i = 0; i < guessLimit; i++)
            {
                _scores[i] = new int[wordLength];
            }
        }

        public void AddScore(int guessNumber, int[] scores)
        {
            if(_scores.Length < guessNumber)
            {
                throw new IndexOutOfRangeException("Can't add score");
            }

            _scores[guessNumber - 1] = scores;
        }

        public void Reset()
        {
            _initialised = false;
            _scores = [];
            _guessLimit = 0;
            _verticalMulitplier = 1;
            _diagonalMulitplier = 1;
        }

        public int GetWordScore(int i)
        {
            if(i == 0)
            {
                return 0; 
            }
            else
            {
                return _scores[i].Sum();
            }
        }

        public int GetVericalScore(int i)
        {
            return _scores.Select(x => x[i]).Sum() * _verticalMulitplier;
        }

        public int GetDiagonalScore(int i, bool reverse)
        {
            var score = 0;

            var rMax = _guessLimit - 1;
            var cMax = reverse ? 0 : _scores[0].Length - 1;
            var r = 0;
            var c = reverse ? _scores[0].Length - 1 - i : i;
            var a = reverse ? -1 : 1;

            while (r <= rMax && c != cMax)
            {
                score += _scores[r][c];

                r++;
                c += a;
            }

            return score * _diagonalMulitplier;
        }

        public int? GetLetterScore(int w, int l)
        {
            if (w == 0)
            {
                return null;
            }
            else
            {
                return _scores[w][l];
            }
        }
    }
}
