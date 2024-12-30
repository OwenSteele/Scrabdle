using Scrabdle.Enums;

namespace Scrabdle.Entities
{
    public static class ConsoleColorMapper
    {
        public static ConsoleColor ToForeground(this ConsoleWriteType type) =>
            type switch
            {
                ConsoleWriteType.Fail => ConsoleColor.Red,
                ConsoleWriteType.Success => ConsoleColor.Green,
                ConsoleWriteType.Info => ConsoleColor.Cyan,
                ConsoleWriteType.Win => ConsoleColor.Cyan,
                ConsoleWriteType.Input => ConsoleColor.Yellow,
                ConsoleWriteType.Magic => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };

        public static ConsoleColor ToBackground(this ConsoleWriteType type) =>
            type switch
            {
                ConsoleWriteType.Win => ConsoleColor.Yellow,
                _ => ConsoleColor.Black
            };
    }
}
