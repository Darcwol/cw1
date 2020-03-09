using System;
using System.Collections;
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
            if(args.Length < 1)
            {
                throw new ArgumentNullException();
            }
            var url = args[0];
            using (var httpClient = new HttpClient())
            {
                try 
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var str = await response.Content.ReadAsStringAsync();

                            var emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                            var emailMatches = emailRegex.Matches(str);

                            Hashtable hash = new Hashtable();

                            foreach (var emailMatch in emailMatches)
                            {
                                string foundMatch = emailMatch.ToString();
                                if (hash.Contains(foundMatch) == false)
                                {
                                    hash.Add(foundMatch, string.Empty);
                                    Console.WriteLine(emailMatch);
                                }
                            }

                            if(emailMatches.Count == 0)
                            {
                                Console.WriteLine("No email found");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error while downloading page");
                        }
                    }
                } catch (InvalidOperationException ex)
                {
                    throw new ArgumentException();
                }
                }
        }
    }
}
