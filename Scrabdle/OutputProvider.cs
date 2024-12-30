using System.Text;
using Microsoft.VisualBasic;
using Scrabdle.Entities;
using Scrabdle.Enums;
using Scrabdle.Stores;

namespace Scrabdle
{
    public class OutputProvider(IGuessStore guessStore, IGameStore gameStore, IDictionaryProcessor dictionaryProcessor)
        : IOutputProvider
    {
        private readonly IGuessStore guessStore = guessStore;
        private readonly IGameStore gameStore = gameStore;
        private readonly IDictionaryProcessor dictionaryProcessor = dictionaryProcessor;
        private const char cDiv = '|';
        private const char rDiv = '-';
        private const char space = ' ';

        public string GetInitialMessage()
        {
            return Constants.InitialMessage;
        }

        public Queue<OutputSection> GetFormattedGuesses()
        {
            var rowCount = gameStore.GuessLimit;
            var columnCount = guessStore.WordLength;
            var guesses = guessStore.GetGuesses();

            var sb = new OutputBuilder();

            var writeType = ConsoleWriteType.None;

            for (var i = 0; i < rowCount; i++)
            {
                var wordScore = 0;

                writeType = i == 0 ? ConsoleWriteType.Info : ConsoleWriteType.None;

                for (var j = 0; j < columnCount; j++)
                {
                    var letter = guesses[i][j];
                    var hasWord = guesses[i][j] != default;

                    var score = gameStore.GetLetterScore(i, j);
                    var subscript = GetSubscript(score);

                    wordScore += score ?? 0;

                    if (subscript.Length <= 1)
                    {
                        sb.Append(space);
                    }

                    sb.Append(hasWord ? letter: ' ', writeType);

                    if (subscript.Length > 0 && hasWord)
                    {
                        sb.Append(subscript, GetScoreColor(score.Value));
                    }
                    else
                    {
                        sb.Append(space);
                    }

                    if (j < columnCount - 1)
                    {
                        sb.Append(cDiv);
                    }
                }

                if(wordScore > 0)
                {
                    sb.Append(space);
                    sb.Append(wordScore, ConsoleWriteType.Info);
                }

                sb.AppendLine();
                sb.AppendLine(new string(rDiv, sb.LongestLineLength));
            }

            var totalScore = gameStore.TotalGuessScore;

            if (totalScore > 0)
            {
                sb.Append(new string(space, sb.LongestLineLength - totalScore.ToString().Length));
                sb.AppendLine(totalScore.ToString(), ConsoleWriteType.Info);
            }

            return sb.GetSections();
        }

        private static string GetSubscript(int? i)
        {
            if (i.HasValue)
            {
                var sb = new StringBuilder();

                foreach (var c in i.Value.ToString().ToCharArray())
                {
                    sb.Append((char)(Constants.SubScriptUni + int.Parse(c.ToString())));
                }

                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }

        }

        public Queue<OutputSection> GetFinalScores()
        {
            var sb = new OutputBuilder();

            var totalScore = gameStore.TotalGuessScore;

            for (var i = 0; i < guessStore.WordLength; i++)
            {
                if (dictionaryProcessor.CheckGuess(guessStore.GetVerticalGuess(i)).Valid)
                {
                    var score = gameStore.GetVericalScore(i);

                    totalScore += score;

                    var strScore = score.ToString();

                    if (strScore.Length == 1)
                    {
                        sb.Append(space);
                    }

                    sb.Append(strScore, ConsoleWriteType.Success);

                    if (strScore.Length == 2)
                    {
                        sb.Append(space);
                    }
                }
                else
                {
                    sb.Append(space);
                    sb.Append('X', ConsoleWriteType.Fail);
                    sb.Append(space);
                }

                if (i < guessStore.WordLength - 1)
                {
                    sb.Append(cDiv);
                }
            }

            sb.Append(space);
            sb.AppendLine(totalScore.ToString());

            sb.Append("Diagonal: ");

            DiagWord(false);
            sb.Append(", ");
            DiagWord(true);
            sb.AppendLine();

            sb.Append("Total Score: ");
            sb.AppendLine(totalScore.ToString());

            void DiagWord(bool reverse)
            {
                if (dictionaryProcessor.CheckGuess(guessStore.GetDiagonalGuess(0, reverse)).Valid)
                {
                    var score = gameStore.GetDiagonalScore(0, reverse);

                    totalScore += score;

                    sb.Append(score.ToString(), ConsoleWriteType.Success);
                }
                else
                {
                    sb.Append(space);
                    sb.Append('X', ConsoleWriteType.Fail);
                    sb.Append(space);
                }
            }          

            return sb.GetSections();
        }

        public static ConsoleWriteType GetScoreColor(int score) => score switch
        {
            1 => ConsoleWriteType.Fail,
            var x when x < 4 => ConsoleWriteType.Input,
            var x when x < 10 => ConsoleWriteType.Success,
            10 => ConsoleWriteType.Magic,
            _ => ConsoleWriteType.None,
        };
    }
}
