using Scrabdle.Entities;

namespace Scrabdle.Validation
{
    public static class InputValidator
    {
        public static InputResult<int> CheckNumeric(string input, bool postiveOnly = false)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new(0, "No Input");
            }
            
            input = input.Trim();
            
            if (int.TryParse(input, out var value))
            {
                if (postiveOnly && value < 1)
                {
                    return new(value, "Number must be greate than zero");
                }
                else
                {
                    return new(value);
                }
            }
            else
            {
                return new(0, "Not a number");
            }
        }

        public static InputResult<bool> CheckDecision(bool? input)
        {
            if (input == null)
            {
                return new(false, "invalid key");
            }

            return new(input.Value);
        }
    }
}
