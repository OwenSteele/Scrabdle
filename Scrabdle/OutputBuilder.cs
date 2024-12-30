using Scrabdle.Entities;
using Scrabdle.Enums;

namespace Scrabdle
{
    public class OutputBuilder
    {
        private readonly Queue<OutputSection> sections;
        private int currentLineLength;

        public int LongestLineLength { get; private set; }
        public int LastLineLength { get; private set; }

    public OutputBuilder()
        {
            sections = [];
        }

        public OutputBuilder(string section, ConsoleWriteType writeType)
        {
            sections = [];

            Append(section, writeType);
        }
        public OutputBuilder(char section, ConsoleWriteType writeType)
        {
            sections = [];

            Append(section, writeType);
        }

        public OutputBuilder(string section)
        {
            sections = [];

            Append(section, default);
        }
        public OutputBuilder(char section)
        {
            sections = [];

            Append(section, default);
        }

        public Queue<OutputSection> GetSections() => sections;

        public OutputBuilder AppendLine() => Append("\r\n", default);
        public OutputBuilder AppendLine(string section, ConsoleWriteType writeType) => Append(section + "\r\n", writeType);
        public OutputBuilder AppendLine(char section, ConsoleWriteType writeType) => Append(section.ToString() + "\r\n", writeType);
        public OutputBuilder AppendLine(int section, ConsoleWriteType writeType) => Append(section.ToString() + "\r\n", writeType);

        public OutputBuilder Append(string section) => Append(section, default);
        public OutputBuilder AppendLine(string section) => AppendLine(section, default);
        public OutputBuilder Append(int section) => Append(section.ToString(), default);
        public OutputBuilder AppendLine(int section) => AppendLine(section.ToString(), default);
        public OutputBuilder Append(char section) => Append(section, default);
        public OutputBuilder AppendLine(char section) => AppendLine(section, default);

        public OutputBuilder Append(string section, ConsoleWriteType writeType)
        {
            sections.Enqueue(new(section, writeType));

            currentLineLength += section.Length;

            if (section.Contains("\r\n"))
            {
                LastLineLength = currentLineLength - 4;
                currentLineLength = 0;
                LongestLineLength = LastLineLength > LongestLineLength ? LastLineLength : LongestLineLength;
            }
            else
            {
                LongestLineLength = currentLineLength;
            }

            return this;
        }

        public OutputBuilder Append(char ch, ConsoleWriteType writeType) => Append(ch.ToString(), writeType);
        public OutputBuilder Append(int section, ConsoleWriteType writeType) => Append(section.ToString(), writeType);


    }
}
