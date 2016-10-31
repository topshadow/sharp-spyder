using System;

namespace CSharpSpyder
{
    public delegate bool Filter(string url);
    public class Program
    {

        public static bool LinkFilter(string url)
        {
            return url.StartsWith("http://www.baidu.com");
        }
        public static void Main(string[] args)
        {
            var seeds = new string[] { "http://www.baidu.com" };
            new Spyder().spyding(seeds);

            Test.RunTest();
            Console.ReadLine();
        }
    }
}
