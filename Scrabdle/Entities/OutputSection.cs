using Scrabdle.Enums;

namespace Scrabdle.Entities
{
    public readonly struct OutputSection
    {
        public string Section { get; }

        public OutputSection(string section, ConsoleWriteType consoleWriteType)
        {
            Section = section;
            WriteType = consoleWriteType;
        }

        public ConsoleWriteType WriteType { get; }
    }
}
