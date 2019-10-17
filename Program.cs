using System;
using System.Collections.Generic;
using System.Linq;
using FaroShuffle.Extensions;

namespace FaroShuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var startingDeck = (from s in Suits().LogQuery("Suit Generada")
                               from r in Ranks().LogQuery("Rank Generado")
                               select new { Suit = s, Rank = r })
                               .LogQuery("Starting Deck").ToArray();
            //var startingDeck = from s in Suits()          from r in Ranks()      select new { Suit = s, Rank = r };
            //var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));                    
 
            var times = 0;
            var shuffle = startingDeck;
            do
            {
                //shuffle = shuffle.Take(26).InterleaveSequenceWith(shuffle.Skip(26));
                shuffle = shuffle.Skip(26).LogQuery("la mitad de abajo")
                        .InterleaveSequenceWith(
                            shuffle.Take(26).LogQuery("la mitad de Arriba")
                        ).LogQuery("Shuffle mix").ToArray();
           
                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine();
                times++;
                Console.WriteLine(times+"...");

             }while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine("Termino en: ");
            Console.WriteLine(times);




        }


        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }

    }
}
