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
            var url = args.Length > 0 ? args[0] : "pja.edu.pl";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
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
    }
}
