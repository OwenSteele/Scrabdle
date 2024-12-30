using Scrabdle.Entities;
using Scrabdle.Enums;

namespace Scrabdle
{
    public static class Writer
    {
        public static void Out() => Out(string.Empty);

        public static void Out(string text, ConsoleWriteType type = ConsoleWriteType.None)
        {
            SetColours(type);
            Console.WriteLine(text);
        }

        public static void Out(Queue<OutputSection> sections)
        {
            foreach (var section in sections)
            {
                SetColours(section.WriteType);
                Console.Write(section.Section.ReplaceLineEndings());
            }
        }

        public static string Prompt(string text, ConsoleWriteType type = ConsoleWriteType.Input)
        {
            SetColours(type);
            Console.Write(text);

            return Console.ReadLine();
        }


        public static bool? PromptDecision(string text, ConsoleWriteType type = ConsoleWriteType.Input)
        {
            SetColours(type);
            Console.Write(text);
            Console.Write(" (y/n) ");

            var key = Console.ReadKey().KeyChar;

            return key == 'y' ? true : key == 'n' ? false : null;
        }

        public static void Clear(int top)
        {
            var currentPosition = Console.GetCursorPosition();

            Console.ForegroundColor = ConsoleWriteType.None.ToForeground();
            Console.BackgroundColor = ConsoleWriteType.None.ToBackground();

            for (int i = top; i <= currentPosition.Top; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, top);
        }

        public static void ClearLines(int count)
        {
            var currentPosition = Console.GetCursorPosition();

            Clear(currentPosition.Top - count);
        }

        private static void SetColours(ConsoleWriteType type) => SetColours(type, type);
        private static void SetColours(ConsoleWriteType fore, ConsoleWriteType back)
        {
            Console.ForegroundColor = fore.ToForeground();
            Console.BackgroundColor = back.ToBackground();
        }
    }
}
