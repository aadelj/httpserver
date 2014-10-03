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
            Console.WriteLine("Server Started");

            Log.WriteInfo("Server Started");

            HttpServer server = new HttpServer();
            server.StartServer();
        }
    }
}
