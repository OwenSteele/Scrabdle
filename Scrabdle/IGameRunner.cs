namespace Scrabdle
{
    public interface IGameRunner
    {
        bool GameOver { get; }
        bool FirstGuess { get; }

        void StartNewGame(int guesses, int wordLength, int verticalMulitplier, int diagonalMulitplier, bool hasStartingWord);
    }
}