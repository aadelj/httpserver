using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8888;

        public void StartServer()
        {
            TcpListener goListener = new TcpListener(DefaultPort);
            goListener.Start();

            TcpClient goTcpClient = goListener.AcceptTcpClient();
            Console.WriteLine("Server Activated");

            Stream ns = goTcpClient.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; //enables automatic flushing

            string message = sr.ReadLine();
            string answer = "";

            string[] words = message.Split(' ');
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
                answer = "<html><body>HTTP/1.0 200 OK</html></body>";
                sw.WriteLine(answer);
                message = sr.ReadLine();
            

            ns.Close();
            goTcpClient.Close();
            goListener.Stop();

        }
    }
}
