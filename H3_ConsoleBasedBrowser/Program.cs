using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H3_ConsoleBasedBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            string htmlResponse;

            Task<string> result = GetWebsite("https://en.wikipedia.org/wiki/Eurasian_otter");
            //If an exception occured, it will not regex the html tags away
            if (!result.IsFaulted)
            {
                htmlResponse = Regex.Replace(result.Result, @"<[^>]*>", "");
            }
            else
                htmlResponse = result.Result;

            Console.WriteLine(htmlResponse);
            Console.ReadLine();

        }

        //Returns the html of a website
        static async Task<string> GetWebsite(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage message = await client.GetAsync(url);
                    message.EnsureSuccessStatusCode();
                    return await message.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
