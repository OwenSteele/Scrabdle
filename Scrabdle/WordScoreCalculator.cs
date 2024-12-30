using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabdle
{
    public static class WordScoreCalculator
    {
        public static int GetWordScore(this string word)
        {
            return word.Sum(x => x.LetterScore());
        }
        public static int[] GetScores(this string word)
        {
            return word.Select(x => x.LetterScore()).ToArray();
        }
    }
}
