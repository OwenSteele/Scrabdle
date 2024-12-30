using Microsoft.VisualBasic;
using Scrabdle.Entities;
using Scrabdle.Enums;
using Scrabdle.Validation;

namespace Scrabdle
{
    public class GameManager(IOutputProvider outputProvider, IInputProcessor inputProcessor, IGameRunner gameRunner) : IGameManager
    {
        private readonly IOutputProvider outputProvider = outputProvider;
        private readonly IInputProcessor inputProcessor = inputProcessor;
        private readonly IGameRunner gameRunner = gameRunner;

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Writer.Out(outputProvider.GetInitialMessage());

            var startPosition = Console.GetCursorPosition();

            while (true)
            {
                Writer.Clear(startPosition.Top);

                Writer.Out("New Game!", ConsoleWriteType.Win);

                var hasStartingWord = GetKeyInput("Random Starting Word Mode");
                var guesses = GetNumericInput("Guess Limit: ");
                var wordLength = GetNumericInput("Word Length: ");
                var vertMult = GetNumericInput("Vertical Word Multiplier: ");
                var diagMult = GetNumericInput("Diagonal Word Multiplier: ");

                gameRunner.StartNewGame(guesses, wordLength, vertMult, diagMult, hasStartingWord);

                Writer.Out(string.Empty);

                var gridPosition = Console.GetCursorPosition();

                Writer.Out(outputProvider.GetFormattedGuesses());

                while (!gameRunner.GameOver)
                {
                    if (gameRunner.FirstGuess)
                    {
                        ProcessGuess("Starting word:");
                    }
                    else
                    {
                        ProcessGuess("Guess: ");
                    }

                    Writer.Clear(gridPosition.Top);

                    Writer.Out(outputProvider.GetFormattedGuesses());
                }

                Writer.ClearLines(2);

                Writer.Out(outputProvider.GetFinalScores());

                Writer.Out("You win... nothing..", ConsoleWriteType.Win);
                Writer.Out("(press any key for new game)", ConsoleWriteType.Success);

                Console.ReadKey();
            }
        }

        private static int GetNumericInput(string message)
        {
            while (true)
            {
                var guessesResult = InputValidator.CheckNumeric(Writer.Prompt(message), true);

                if (guessesResult.Valid)
                {
                    return guessesResult.Input;
                }
                else
                {
                    Writer.Out(guessesResult.Message, ConsoleWriteType.Fail);
                }
            }
        }

        private static bool GetKeyInput(string message)
        {
            while (true)
            {
                var guessesResult = InputValidator.CheckDecision(Writer.PromptDecision(message));

                if (guessesResult.Valid)
                {
                    Writer.Out();

                    return guessesResult.Input;
                }
                else
                {
                    Writer.Out(guessesResult.Message, ConsoleWriteType.Fail);
                }
            }
        }


        private InputResult<string> ProcessGuess(string message)
        {
            while (true)
            {
                var guessesResult = inputProcessor.ProcessGuess(Writer.Prompt(message));

                if (guessesResult.Valid)
                {
                    return guessesResult;
                }
                else
                {
                    Writer.Out(guessesResult.Message, ConsoleWriteType.Fail);
                }
            }
        }
    }
}
