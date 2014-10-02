using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer();
            server.StartServer();

            Console.WriteLine("Hello http server");
        }
    }
}
