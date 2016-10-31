using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Net;
using System;
using System.Text;
using System.Text.Encodings;
// using System.Text.Code
namespace BXWXSpyder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var spyder = new Spyder();
            // 控制台字符编码
            Console.OutputEncoding = Encoding.UTF8;
            //解析文本的字符串编码gbk添加额外的字符库
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            for (var i = 1; i < 150; i++)
            {
                spyder.addBookUrl("http://www.bxwx.com", "list1", i.ToString());
            }
            // spyder.start();
            foreach (var book in spyder.seeds)
            {
                spyder.parseHtml(book);
            }

            foreach (var url in spyder.books)
            {
                Console.WriteLine(url);
            }
            Console.WriteLine($"总共书的数量:${spyder.books.ToArray().Length}");

            Console.ReadLine();
            // foreach (var seed in spyder.seeds)
            // {
            //     Console.WriteLine(seed);
            // }


        }

        static async void getResult()
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://www.bxwx.com");
            if (response.IsSuccessStatusCode)
            {
                Console.OutputEncoding = Encoding.GetEncoding("GBK");
                // Encoding gbk = Encoding.GetEncoding("GBK");
                Console.WriteLine($"请求成功~请求头是{response.StatusCode}.响应解析是{response.ReasonPhrase}");
                var resultBytes = response.Content.ReadAsByteArrayAsync().Result;
                var gbk = Encoding.GetEncoding("gb2312");
                var utf16 = Encoding.Unicode;
                var dirname = @"./dist";
                if (!Directory.Exists(dirname))
                {
                    Directory.CreateDirectory(dirname);
                }
                File.WriteAllText(dirname + "/index.html", gbk.GetString(resultBytes), gbk);
            }
            else
            {
                Console.WriteLine("错误,请求未成功");
            }
        }
    }
}
