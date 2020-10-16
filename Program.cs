using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace DucBui_CrawlerCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            //Crawler to get content website
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString("https://en.wikipedia.org/wiki/Microsoft"));
            var root = html.DocumentNode;
            var content = root.InnerText;

            //Format text, remove number and space
            content = RemoveSpecialCharacters(content);            
            //Using Dictionary to count 
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string[] arr = content.Split(' ');
            
            foreach (string item in arr)
            {
                if(!dic.ContainsKey(item))
                {
                    dic.Add(item,1);
                }
                else
                {
                    dic[item] += 1;
                }
            }            
            //Get Top 10 Dictionlary
            var order = dic.OrderByDescending(x => x.Value)
                        .Take(10)
                        .ToDictionary(pair => pair.Key, pair => pair.Value);
            //Print result
            Console.WriteLine(">>> The Result >>>");
            foreach (KeyValuePair<string,int> pair in order)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }            
            Console.ReadLine();
        }
        public static string RemoveSpecialCharacters(string input)
        {
            //Remove special character and number
            Regex r = new Regex("(?:[^a-zA-Z]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            string sentence =  r.Replace(input, " ");
            //Remove space charactor
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            return regex.Replace(sentence, " ");
        }
    }
}
