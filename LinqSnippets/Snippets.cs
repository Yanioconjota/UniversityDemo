using Microsoft.Extensions.Hosting;
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
        //paging with skip & take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            if (collection == null)
            { return Enumerable.Empty<T>(); }


            int startIdex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIdex).Take(resultsPerPage);
        }

        //Variables
        static public void LinqqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square {1}", number, Math.Pow(number, 2));
            }

        }

        //ZIP

        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);

            // {"1 = one", "2 = two",...}
        }


        //Repeat & Range

        static public void RepeatRangeLinq(int times)
        {
            // Generate a collection of values from 1 - 1000
            IEnumerable<int> first1000 = Enumerable.Range(0, 1000);

            // Repeat a value N times
            IEnumerable<string> xTimes = Enumerable.Repeat("X", times); // {"x","x","x","x","x"...}

        }

        static public void StudentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Yoda",
                    Grade = 95,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Qui-Gon",
                    Grade = 75,
                    Certified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "Obi-Wan",
                    Grade = 80,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Anakin",
                    Grade = 99,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Ahsoka",
                    Grade = 78,
                    Certified = false,
                }
            };

            var certifiedStudents = from Student student in classRoom
                                    where student.Certified == true
                                    select student;

            var notCertifiedStudents = from Student student in classRoom
                                       where student.Certified == false
                                       select student;

            IEnumerable<(string Name, int Grade)> approvedStudents = from Student student in classRoom
                                                                     where student.Grade >= 50 && student.Certified == true
                                                                     select (student.Name, student.Grade);
        }

        //ALL
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allSmallerThan10 = numbers.All(x => x < 10); // true

            bool allLargerOrEqualThan2 = numbers.All(x => x > 2); // false


            var emptyList = new List<int>();

            bool allNumbersGreaterThanZero = numbers.All(x => x > 0); // true
        }

        //Aggreagte
        static public void aggregateQueries()
        {
            //Accumalative sequence of functions, the output of an operations is the input of the next one
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7
            // ...

            string[] words = { "Hello,", "my", "name", "is", "C-3P0" };// Hello, my name is C-3P0
            string greetings = words.Aggregate((prevWord, nextWord) => prevWord + nextWord);

            // "", "Hello," => Hello,
            // "Hello,", "my," => Hello, my
            // "Hello, my", "name," => Hello, my name
            // "Hello, my name", "is," => Hello, my name is
            // "Hello, my name is", "C-3P0," => Hello, my name is C-3P0
        }

        //Distinct
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };

            IEnumerable<int> uniqueValues = numbers.Distinct();
        }

        //GroupBy
        static public void groupByExample()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtain only even numbers and generate two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // We will have 2 groups
            // 1. The first that fits the conditions
            // 2. The secont is the one that doesn't

            foreach (var group in grouped)
            {
                foreach (var item in group)
                {
                    Console.WriteLine(item); // 1, 3, 5, 7, 9 ... 2, 4, 6, 8 (first odds and then even)
                }
            }

            // Students example:
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Yoda",
                    Grade = 95,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Qui-Gon",
                    Grade = 75,
                    Certified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "Obi-Wan",
                    Grade = 80,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Anakin",
                    Grade = 99,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Ahsoka",
                    Grade = 78,
                    Certified = false,
                }
            };

            var approvedStudentsQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            // Two groups:
            // 1. Not Approved group
            // 2. Approved group

            foreach (var group in approvedStudentsQuery)
            {
                Console.WriteLine("---------{0}---------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine($"Student: {student}");
                }
            }
        }

        public static void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    CreatedDate = DateTime.Now,
                    Comments = new List<Comment>
                    {
                        new Comment()
                        {
                            Id = 1,
                            CreatedDate = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content bla, bla, bla, bla, bla, bla"
                        },
                        new Comment()
                        {
                            Id = 2,
                            CreatedDate = DateTime.Now,
                            Title = "Another comment",
                            Content = "My content bla, bla, bla, bla, bla, bla"
                        },
                        new Comment()
                        {
                            Id = 3,
                            CreatedDate = DateTime.Now,
                            Title = "Yet another comment",
                            Content = "My content bla, bla, bla, bla, bla, bla"
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    CreatedDate = DateTime.Now,
                    Comments = new List<Comment>
                    {
                        new Comment()
                        {
                            Id = 4,
                            CreatedDate = DateTime.Now,
                            Title = "My first comment in this post",
                            Content = "My content bla, bla, bla, bla, bla, bla"
                        },
                        new Comment()
                        {
                            Id = 5,
                            CreatedDate = DateTime.Now,
                            Title = "Another comment in this post",
                            Content = "My content bla, bla, bla, bla, bla, bla"
                        },
                        new Comment()
                        {
                            Id = 6,
                            CreatedDate = DateTime.Now,
                            Title = "Yet another comment in this post",
                            Content = "My content bla, bla, bla, bla, bla, bla..."
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments,
                (post, comment) => new { PostId = post.Id, CommentContent = post.Content }
            );
        }
    }
}