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

        private static readonly string RootCatalog = @"c:/temp";

        //public void CopyTo(Stream RootCatalog)
        //{

        //    MemoryStream destination = new MemoryStream();

        //    using (FileStream source = File.Open(@"c:/temp/adel.txt", FileMode.Open, FileAccess.Read))
        //    {
        //        Console.WriteLine("Source Length: {0}", source.Length.ToString());

        //        source.CopyTo(destination);
        //    }
        //    Console.WriteLine("Destination length: {0}", destination.Length.ToString());
        //}
        

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
            FileStream source = File.Open(@"c:/temp/httpass.htm", FileMode.Open, FileAccess.Read);
            source.CopyTo(sw.BaseStream);
            source.Flush();
                //answer = "<html><body>HTTP/1.0 200 OK</html></body>";
                //sw.WriteLine(answer);
                //message = sr.ReadLine();
            

            ns.Close();
            goTcpClient.Close();
            goListener.Stop();

        }
    }
}
