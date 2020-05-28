using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace ASPDotNET
{
    /// <summary>
    /// https://docs.microsoft.com/zh-tw/aspnet/web-api/overview/older-versions/self-host-a-web-api
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            //架起Server
            using (HttpSelfHostServer server = new HttpSelfHostServer(MySetting.getServerConfig()))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
