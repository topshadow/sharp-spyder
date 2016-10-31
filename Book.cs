using System;
namespace BXWXSpyder
{
    public class Book
    {
        public string name { get; set; }

        public string url { get; set; }
        public string type { get; set; }
        public string code { get; set; }
        public Book(string origin, string type, string code)
        {
            this.url = $"{origin}/{type}/{code}/";
            this.type = type;
            this.code = code;

        }


        public override string ToString()
        {
            return $"url:{this.url},name:{this.name},type:{this.type},code:{this.code}";
        }
    }
}