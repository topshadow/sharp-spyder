using System.Collections;
using System.Collections.Generic;
using System;
namespace CSharpSpyder
{
    public class MyQueue
    {
        private LinkedList<string> queue = new LinkedList<string>();
        public void enQueue(string url)
        {
            this.queue.AddLast(url);
        }
        public string deQueue()
        {
            var first = this.queue.First.Value;
            this.queue.RemoveFirst();
            return first;
        }
        public bool isEmpty()
        {
            return this.queue.Count == 0;
        }
        public bool contains(string url)
        {
            return this.queue.Contains(url);
        }
    }

    public class LinkQueue
    {
        // 已经访问过的url 集合
        private static HashSet<string> visitedUrl = new HashSet<string>();
        private static MyQueue unVisitedUrl = new MyQueue();

        public static MyQueue getUnVisitedUrl()
        {
            return LinkQueue.unVisitedUrl;
        }

        public static string dequeueUnVisitedUrl()
        {
            return LinkQueue.unVisitedUrl.deQueue();
        }

        public static void addVisitedUrl(string url)
        {
            visitedUrl.Add(url);
        }
        public static void addUnVisitedUrl(string url)
        {
            // Console.WriteLine($"${!visitedUrl.Contains(url)} {unVisitedUrl.contains(url)} {url != null}");
            if ((!visitedUrl.Contains(url)) && !(unVisitedUrl.contains(url)) && (url != "") && (url != null))
            {

                unVisitedUrl.enQueue(url);
            }
            else
            {
                Console.WriteLine($"爬到重复的链接:{url}");
            }
        }

        public static void removeVisitedUrl(string url)
        {
            visitedUrl.Remove(url);
        }
        public static int visitedUrlNum
        {
            get
            {
                return visitedUrl.Count;
            }
        }


    }


}