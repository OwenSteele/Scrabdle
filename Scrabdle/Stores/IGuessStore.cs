namespace Scrabdle.Stores
{
    public interface IGuessStore
    {
        int WordLength { get; }
        int Guesses { get; }

        void AddGuess(string guess);
        void AddGuess(char[] guess);
        string GetDiagonalGuess(int i, bool reverse);
        char[][] GetGuesses();
        int GetGuessIndex(char[] letters);
        string GetVerticalGuess(int i);
        void Init(int guessesLimit, int wordLength, string startingWord);
        void Reset();
    }
}