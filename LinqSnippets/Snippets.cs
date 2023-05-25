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

            //Obtain { 4, 8 }
            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // { 4, 8 }
            Console.WriteLine(myEvenNumbers);
        }

        static public void MultiSelects()
        {
            //SLECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Tony",
                            Email = "tonysoprano@gmail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Paulie",
                            Email = "pauliegualtieri@gmail.com",
                            Salary = 2500
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Dante",
                            Email = "silviosante@gmail.com",
                            Salary = 2500
                        },
                        new Employee
                        {
                            Id = 4,
                            Name = "Chris",
                            Email = "chrismoltisanti@gmail.com",
                            Salary = 2000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 5,
                            Name = "Johnny",
                            Email = "johnnysacrimoni@gmail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Phil",
                            Email = "philleotardo@gmail.com",
                            Salary = 2500
                        },
                        new Employee
                        {
                            Id = 7,
                            Name = "Rusty",
                            Email = "rustymillio@gmail.com",
                            Salary = 2500
                        },
                        new Employee
                        {
                            Id = 8,
                            Name = "James",
                            Email = "jamespetrille@gmail.com",
                            Salary = 2000
                        }
                    }
                }
            };

            //Obtain all employees from all enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            //Know if a list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //All employees with at least 2500$ salary
            bool hasEmployeeSalaryEqualOrGreaterThan2500 =
                    enterprises.Any(enterprises => enterprises.Employees.Any(employee => employee.Salary >= 2500));
        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            //INNER JOIN --> Matching elements in both list
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                );

            //OUTTER JOIN --> LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into tempList
                                from tempElement in tempList.DefaultIfEmpty()
                                where element != tempElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(e => e == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };

            //OUTTER JOIN --> RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into tempList
                                 from tempElement in tempList.DefaultIfEmpty()
                                 where secondElement != tempElement
                                 select new { Element = secondElement };

            var rightOuterJoin2 = from secondElement in secondList
                                  from element in firstList.Where(e => e == secondElement).DefaultIfEmpty()
                                  select new { SecondElement = secondElement, Element = element };

            //UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            //SKIP

            var skipFirstValues = myList.Skip(2); // { 3,4,5,6,7,8,9,10 }
            
            var skipLastValues = myList.SkipLast(2); // { 1,2,3,4,5,6,7,8 }

            var skipWhile = myList.SkipWhile(num => num < 4); // { 4,5,6,7,8,9,10 }

            //TAKE
            var takeFirstValues = myList.Take(2); // { 1,2 }

            var takeLastValues = myList.TakeLast(2); // { 9,10 }

            var takeWhile = myList.TakeWhile(num => num < 4); // { 1,2,3 }
        }

        //TODO:

        //Variables

        //ZIP

        //Repeat

        //ALL

        //Aggreagte

        //Distinct

        //GroupBy
    }
}