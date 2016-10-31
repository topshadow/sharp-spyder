using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using HtmlAgilityPack;
using System;
namespace CSharpSpyder
{
    public class HtmlParser
    {
        public static HttpClient client = new HttpClient();
        public static HashSet<string> extractLinks(string url)
        {
            var response = client.GetAsync(url).Result;
            Console.WriteLine($"开始解析页面{url}");
            var html = response.Content.ReadAsStringAsync().Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var links = new HashSet<string>();
            foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute attr = link.Attributes["href"];
                links.Add(attr.Value.ToString());
            }
            Console.WriteLine($"解析完了{url},{links}");
            return links;
        }
    }
}