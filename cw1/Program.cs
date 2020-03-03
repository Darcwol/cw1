using System;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://www.pja.edu.pl/");
            if(response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();

                var emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                var emailMatches = emailRegex.Matches(str);

                foreach (var emailMatch in emailMatches)
                {
                    Console.WriteLine(emailMatch);
                }
            }
        }
    }
}
