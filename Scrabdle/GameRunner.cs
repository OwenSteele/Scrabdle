using Scrabdle.Enums;
using Scrabdle.Proxies;
using Scrabdle.Stores;

namespace Scrabdle
{
    public class GameRunner(
        IHerokuApiProxy herokuApiProxy,
        IGuessStore guessStore,
        IGameStore gameStore)
        : IGameRunner
    {
        private readonly IHerokuApiProxy _herokuApiProxy = herokuApiProxy;

        public bool GameOver => gameStore.GuessLimit <= guessStore.Guesses;
        public bool FirstGuess => guessStore.Guesses == 0;

        public void StartNewGame(int guesses, int wordLength, int verticalMulitplier, int diagonalMulitplier, bool hasStartingWord)
        {
            var startingWord = string.Empty;

            if (hasStartingWord)
            {
                var startingWordResponse = _herokuApiProxy.GetRandomWordAsync(wordLength).GetAwaiter().GetResult();

                if (startingWordResponse.Success)
                {
                    startingWord = startingWordResponse.Word.ToUpper();
                }
                else
                {
                    Writer.Out($"Failed to get random Starting word: {startingWordResponse.Error}", ConsoleWriteType.Fail);
                }
            }
            
            guessStore.Reset();
            guessStore.Init(guesses, wordLength, startingWord);

            gameStore.Reset();
            gameStore.Init(guesses, wordLength, verticalMulitplier, diagonalMulitplier, startingWord);
        }
    }
}
