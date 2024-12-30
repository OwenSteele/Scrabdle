using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabdle.Entities
{
    public readonly struct InputResult<TInput>
    {
        public InputResult(TInput input, string error)
        {
            Input = input;
            Valid = false;
            Message = error;
        }

        public InputResult(TInput input)
        {
            Input = input;
            Valid = true;
            Message = string.Empty;
        }

        public readonly bool Valid { get; }
        public readonly string Message { get; }
        public readonly TInput Input { get; }
    }
}
