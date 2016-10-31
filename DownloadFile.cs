using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text;
namespace CSharpSpyder
{
    public class DownloadFile
    {
        private static HttpClient client = new HttpClient();

        public DownloadFile(string url)
        {

            downlaod(url);
        }

        /// <summary>
        /// 自动修正链接
        /// 例如 //www.baidu.com  
        ///  http://
        /// </summary>
        /// <param name="url"></param>
        public void downlaod(string url)
        {



            Console.WriteLine($"下载的链接是:{url}");

            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsByteArrayAsync().Result;
            var filename = this.getFileNameByUrl(url);
            this.saveToLocal(data, filename);
            Console.WriteLine($"下载了文件{filename}");
        }

        /// <summary>
        /// 根据链接提取文件名
        /// </summary>
        /// <example>
        ///  指定媒体类型
        /// </example>
        /// <param name="string"></param>
        /// <returns></returns>
        public string getFileNameByUrl(string url)
        {

            url = url.Substring(7);
            var filterChar = new Regex("[\\?/:*|<>\"]");

            if (url.EndsWith(".html"))
            {
                return filterChar.Replace(url, "_") + ".html";

            }
            // 如application/pdf
            else
            {

                return filterChar.Replace(url, "");
            }
        }


        /// <summary>
        /// 字符集问题,需要在spyder初始化的时候,指定字符集，并且提示用户默认字符集是多少
        /// 还要考虑指定网页下载文件夹的问题
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        private void saveToLocal(byte[] data, string filePath)
        {
            if (!Directory.Exists("./dist"))
            {
                Directory.CreateDirectory("./dist");
            }
            File.WriteAllBytes("./dist/" + filePath, data);



        }
    }
}