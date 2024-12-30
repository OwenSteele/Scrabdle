namespace Scrabdle.Entities
{
    public readonly struct RandomWordResponse
    {
        public string Word { get; }
        public string Error { get; }
        public bool Success { get; }

        public RandomWordResponse(string word)
        {
            Error = string.Empty;
            Word = word;
            Success = true;
        }

        public RandomWordResponse(bool success, string error)
        {
            Error = error;
            Word = string.Empty;
            Success = success;
        }

    }
}
