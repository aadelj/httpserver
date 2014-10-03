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

        private bool serverOn;

        private static readonly string RootCatalog = @"c:/temp";

        public void StartServer()
        {
            serverOn = true;

            TcpListener goListener = new TcpListener(DefaultPort);
            goListener.Start();

            while (serverOn)
            {
                TcpClient goTcpClient = goListener.AcceptTcpClient();
                Console.WriteLine("Server Activated");

                Stream ns = goTcpClient.GetStream();
                StreamReader sr = new StreamReader(ns);
                try
                {
                    StreamWriter sw = new StreamWriter(ns);
                    sw.AutoFlush = true; //enables automatic flushing
                    string message = sr.ReadLine();
                    string answer = "";
                    string[] words = message.Split(' ');
                    foreach (string word in words)
                    {
                        Console.WriteLine(word);
                    }
                    string filePath = RootCatalog + words[1];
                    

                    if (!File.Exists(filePath))
                    {
                        sw.Write("HTTP/1.0 404 Not Found\r\n\r\n");
                        message = sr.ReadLine();
                    }
                    else
                    {
                        answer = "HTTP/1.0 200 OK\r\n\r\n";
                        sw.WriteLine(answer);
                        message = sr.ReadLine();
                        using (FileStream source = File.Open(filePath, FileMode.Open, FileAccess.Read))
                        {
                            source.CopyTo(sw.BaseStream);
                            source.Flush();
                        }
                        Console.WriteLine(message);
                    }
                    sr.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Unidentified Error - HTTP/1.0 404 Not Found");
                }
                finally
                {
                    ns.Close();
                    goTcpClient.Close();
                    
                }
            }
            goListener.Stop();
        }
    }
}
            
            
        
           
    

