using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8888;

        private static readonly string RootCatalog = @"c:/temp";

       
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
            string temp = RootCatalog + words[1];
            FileInfo fi = new FileInfo(temp);
            if (fi.Exists)
            {
                FileStream source = File.Open(RootCatalog + words[1], FileMode.Open, FileAccess.Read);

                source.CopyTo(sw.BaseStream);
                source.Flush();
            }
            else
            {
                sw.WriteLine(message);
            }

            //FileStream source = File.Open(RootCatalog + words[1], FileMode.Open, FileAccess.Read);

            //source.CopyTo(sw.BaseStream);
            //source.Flush();
            answer = "HTTP//1.0 200 OK";
            sw.WriteLine(answer);
            message = sr.ReadLine();
                

            ns.Close();
            goTcpClient.Close();
            goListener.Stop();

        }
    }
}
