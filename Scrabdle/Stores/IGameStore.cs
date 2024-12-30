namespace Scrabdle.Stores
{
    public interface IGameStore
    {
        int GuessLimit { get; }
        int TotalGuessScore { get; }
        string StartingWord { get; }

        void AddScore(int index, int[] scores);
        int GetDiagonalScore(int i, bool reverse);
        int? GetLetterScore(int i, int j);
        int GetVericalScore(int i);
        int GetWordScore(int i);
        void Init(int guessLimit, int wordLength, int verticalMulitplier, int diagonalMulitplier, string startingWord);
        void Reset();
    }
}