using System;
using System.Linq;
using static System.Console;

namespace Coding.Exercise
{
    public class Sentence
    {
        private WordToken[] tokens;
        private string[] strs;
        public Sentence(string plainText)
        {
            // todo
            strs = plainText.Split(' ');
            tokens = strs.Select(_ => new WordToken()).ToArray();
        }

        public WordToken this[int index]
        {
            get
            {
                // todo
                return this.tokens[index];
            }
        }

        public override string ToString()
        {
            // output formatted text here
            var res = strs.Select((str, idx) => tokens[idx].Capitalize ? str.ToUpper() : str).ToArray();
            return string.Join(" ", res);
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var sentece = new Sentence("hello world");
            sentece[1].Capitalize = true;
            Console.WriteLine(sentece);

        }
    }
}
