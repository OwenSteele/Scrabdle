namespace Scrabdle.Entities
{
    public readonly struct DictionaryApiResponse
    {
        public bool Success { get; }

        public string Message { get; }

        public bool WordFound { get; }

        public DictionaryApiResponse(string error)
        {
            Success = false;
            Message = error;
            WordFound = false;
        }

        public DictionaryApiResponse(bool wordFound, string message)
        {
            Success = true;
            Message = message;
            WordFound = wordFound;
        }

        public DictionaryApiResponse()
        {
            Success = true;
            Message = string.Empty;
            WordFound = true;
        }
    }
}
