using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace _12rules
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running main");
            var rule = new Rule12();
            rule.Demo();

            // Rule 8
            [Conditional("DEBUG")]
            static void DoSomething()
            {
                Console.WriteLine("Doing something");
            }
            DoSomething();

            Console.ReadLine();
        }

        [ModuleInitializer]
        public static void RunBeforeMain()
        {
            Console.WriteLine("rule 11!");
        }
    }
    unsafe class FunctionPointer
    {
        static int GetLength(string s) => s.Length;

        delegate*<string, int> functionPointer = &GetLength;

        public void Test()
        {
            var result = functionPointer("12rules");
        }
    }
    class Rule12
    {
        public void Demo()
        {
            var fnPointer = new FunctionPointer();
            fnPointer.Test();
        }
    }
    class Rule10
    {
        public abstract class Fruit
        {
            protected string Name { get; }
            protected int Type { get; }

            protected Fruit(string name, int type)
            {
                Name = name;
                Type = type;
            }

            public abstract FruitOrder Order(int quantity);
        }

        public class Apple : Fruit
        {
            public string Country { get; }

            public Apple(string name, int type, string country) : base(name, type)
            {
                Country = country;
            }

            public override AppleOrder Order(int quantity) => new AppleOrder { Quantity = quantity, Fruit = this };
        }

        public class FruitOrder
        {
            public int Quantity { get; set; }
        }

        public class AppleOrder : FruitOrder
        {
            public Apple Fruit { get; set; }
        }

        public void Demo()
        {
            var apple = new Apple("Granny Smith", 2, "Australia");

            AppleOrder orderApple = apple.Order(1);
        }

    }
    partial class Part
    {
        internal partial bool DoSomething(string s, out int i);
    }
    partial class Part
    {
        internal partial bool DoSomething(string s, out int i)
        {
            i = 2;
            return true;
        }
    }
    class Rule9
    {
        public void Demo()
        {
            Part p = new Part();
            int val;
            p.DoSomething("abc", out val);
        }
    }
    public static class Extensions
    {
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;
    }
    class Rule7
    {
        public void Demo()
        {
            var letters = new Collection<string> { "A", "B", "C" }.GetEnumerator();

            foreach (var letter in letters)
            {
                Console.WriteLine(letter);
            }
        }
    }
    class Rule6
    {
        const string text = "{0} is the best!"; 

        void Promote(Func<string, string> func)
        {
            var bestThings = new List<string> { "Blazor", "C#" };

            foreach (var bestThing in bestThings)
                Console.WriteLine(func(bestThing));
        }
        public void Demo()
        {
            Promote(country => string.Format(text, country));

            Promote(static country => string.Format(text, country));
        }
    }
    class Rule5
    {
        public void Demo()
        {
            List<int> l = new();
            l.Add(2);

            Point p = new(1, 1);
            Point[] points = { p, new(2, 2), new(3, 3) };
        }
    }
    class Rule4
    {
        public class Thing
        {
            public string A { get; init; }
            public int B { get; init; }
        }
        public void Demo()
        {
            var thing1 = new Thing
            {
                A = "abc",
                B = 7
            };
        }
    }
    class Rule3
    {
        public void Demo()
        {
            nint nativeInt = 5;

            Console.WriteLine(nint.MaxValue);
            Console.WriteLine(nuint.MaxValue);

            /*
            x86
            2147483647
            4294967295

            x64
            9223372036854775807
            18446744073709551615
            */
        }
    }
    class Rule2
    {
        public record Val(bool Positive, int Number);

        public void Demo()
        {
            // type pattern
            object thing1 = new int();
            var thing1Type = thing1 switch
            {
                string => "string",
                int => "integer",
                _ => "object"
            };
            Console.WriteLine("type pattern: " + thing1Type);

            // or pattern
            var thing2 = 7;
            var thing2Or = thing2 switch
            {
                < 5 => "less than 5",
                5 or > 5 => "equal or greater than 5"
            };
            Console.WriteLine("or pattern: " + thing2Or);

            // and pattern
            var thing3 = 6;
            var thing3And = thing3 switch
            {
                < 5 and > 3 => "lt 5 gt 3",
                < 7 => "lt 7"
            };
            Console.WriteLine("and pattern: " + thing3And);

            // not pattern
            var thing4 = 12;
            var thing4Not = thing4 switch
            {
                not 10 => "not 10",
                10 => "is 10"
            };
            Console.WriteLine("not pattern: " + thing4Not);

            // parenthesized pattern
            var thing5 = new Val(true, 5);
            var thing5Parenthesized = thing5 switch
            {
                ((_, > 2) and (_, > 4 and < 8)) or (_, 7) => "good",
                _ => "not good"
            };
            Console.WriteLine("parenthesized pattern: " + thing5Parenthesized);

            // relational pattern
            var thing6 = new Val(false, 7);
            var thing6relational = thing6 switch
            {
                Val(_, < 5) => "lt 5",
                (_, > 5) => "gt 5",
                (_, 5) => "eq 5"
            };
            Console.WriteLine("relational pattern: " + thing6relational);
        }
    }
    class Rule1
    {
        public record Thing(string A, int B);

        public void Demo()
        {
            var thing1 = new Thing("abc", 7);
            var thing2 = new Thing("abc", 7);

            // true or false? True
            Console.WriteLine(thing1 == thing2);

            // true or false? True
            Console.WriteLine(thing1.Equals(thing2));

            // true or false? False
            Console.WriteLine(ReferenceEquals(thing1, thing2));

            var thing3 = thing1 with { B = 14 };

            // true or false? False
            Console.WriteLine(thing1 == thing3);
        }
    }
}
