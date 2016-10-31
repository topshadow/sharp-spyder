using System.Net;
using System;

using System.Net.Http;
namespace CSharpSpyder
{
    public interface LinkFilter
    {
        bool accept(string url);
    }
    public class Spyder
    {

        public Spyder()
        {


        }
        /// <summary>
        /// 初始化爬虫的配置
        /// </summary>
        public void init()
        {


        }

        public void spyding(string[] seeds)
        {
            var filter = new Filter((url) =>
            {
                return url.StartsWith("http://www.baidu.com");

            });
            //初始化爬虫种子
            this.initSeeds(seeds);
            while ((!LinkQueue.getUnVisitedUrl().isEmpty()) && LinkQueue.visitedUrlNum < 1000)
            {
                string visitUrl = LinkQueue.dequeueUnVisitedUrl();
                Console.WriteLine($"开始爬取:{visitUrl}");
                if (visitUrl == null)
                {
                    Console.WriteLine("visitUrl为空,跳出当前循环");
                    continue;
                }
                new DownloadFile(visitUrl);
                LinkQueue.addVisitedUrl(visitUrl);
                var links = HtmlParser.extractLinks(visitUrl);

                Console.WriteLine($"抓取链接");
                foreach (var link in links)
                {
                    var fixLink = link.StartsWith("//") ? "http:" + link : link;
                    LinkQueue.addUnVisitedUrl(fixLink);
                }

            }
        }
        public void initSeeds(string[] seeds)
        {
            foreach (var seed in seeds)
            {
                LinkQueue.addUnVisitedUrl(seed);
            }
        }

    }

}