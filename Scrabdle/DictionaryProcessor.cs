using Scrabdle.Entities;
using Scrabdle.Proxies;

namespace Scrabdle
{
    public class DictionaryProcessor(IDictionaryApiProxy dictionaryApiProxy) : IDictionaryProcessor
    {
        private readonly IDictionaryApiProxy dictionaryApiProxy = dictionaryApiProxy;

        public InputResult<string> CheckGuess(string guess)
        {
            try
            {
                var result = dictionaryApiProxy.CheckWordAsync(guess).GetAwaiter().GetResult();

                if (result.Success && result.WordFound)
                {
                    return new(guess);
                }
                else
                {
                    return new(guess, result.Message);
                }
            }
            catch (Exception ex)
            {
                return new(guess, ex.Message);
            }
        }
    }
}
