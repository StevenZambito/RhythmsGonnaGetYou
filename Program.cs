using System;
using System.Linq;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EnigmaRecordsContext();

            var Artists = context.Artists;

            Console.WriteLine(Artists);
        }
    }
}
