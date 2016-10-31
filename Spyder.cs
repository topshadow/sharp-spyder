using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
namespace BXWXSpyder
{
    public class Spyder
    {
        public List<Book> seeds;
        public HttpClient client = new HttpClient();
        public List<string> books = new List<string>();

        public Spyder()
        {
            this.seeds = new List<Book>();
        }


        public void addBookUrl(string origin, string type, string name)
        {
            var book = new Book(origin, type, name);
            this.seeds.Add(book);
        }
        public void start()
        {
            this.seeds.ForEach(book =>
            {
                this.downloadFile(book);
            });
            // var book = this.seeds[0];
            // downloadFile(book);
        }

        public async void downloadFile(Book book)
        {
            Console.WriteLine(book);
            var response = await this.client.GetAsync(book.url);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"请求status是:{response.StatusCode},{response.ReasonPhrase}");
                var resultBytes = response.Content.ReadAsByteArrayAsync().Result;
                var gbk = Encoding.GetEncoding("GBK");
                var result = gbk.GetString(resultBytes);
                if (!Directory.Exists($"./dist/{book.type}"))
                {
                    Directory.CreateDirectory($"./dist/{book.type}");
                }
                File.WriteAllText($"./dist/{book.type}/{book.code}.html", result, gbk);

            }
            else
            {
                Console.WriteLine("请求错误");
            }
        }

        public void parseHtml(Book book)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(File.ReadAllText($"./dist/{book.type}/{book.code}.html", Encoding.GetEncoding("GBK")));
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes($"//*[@class=\"clearfix rec_rullist\"]/ul/li/a"))
            {
                HtmlAttribute attr = link.Attributes["href"];

                Console.WriteLine(attr.Value);
                this.books.Add(attr.Value);

            }

        }
    }

}