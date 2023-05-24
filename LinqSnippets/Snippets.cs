using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        //String examples
        static public void BasinLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            //1. SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE car is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var audi in audiList)
            {
                Console.Write(audi);
            }

        }

        //Nmber examples
        static public void LinQNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Each number mulplied by 3
            //Take all numbers but 9
            //Order numbers by ascending value

            var processedNumberList = numbers
                .Select(num => num * 3) //{3, 6, 9, etc}
                .Where(num => num != 9) //{all but 9}
                .OrderBy(num => num); // order by asc
        }

        //Search examples

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c",
            };

            //1. First of all elements
            var first = textList.First();

            //2. First element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            //3. First element that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            //4. First element that contains "z" or default value
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); //"" or first element containing "z"

            //5. Last element that contains "z" or default value
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); //"" or last element containing "z"

            //6. Single value
            var uniqueText = textList.Single();
            var uniqueOrDefaultText = textList.SingleOrDefault();
        }
    }
}