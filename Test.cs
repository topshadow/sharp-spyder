using System.IO;
using System;
using System.Buffers;
using System.Text.RegularExpressions;

namespace CSharpSpyder
{
    public class Test
    {
        public static void RunTest()
        {
            var links = HtmlParser.extractLinks("http://map.baidu.com/");
            foreach (var link in links)
            {
                Console.WriteLine(link);
            }

        }
    }
}