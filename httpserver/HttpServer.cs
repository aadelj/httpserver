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
        /// <summary>
        /// We create a static port and declare the Default port.
        /// </summary>
        public static readonly int DefaultPort = 8888;

        /// <summary>
        /// Used to define that the server is active.
        /// </summary>
        private bool serverOn;

        /// <summary>
        /// The root-folder of the files.
        /// </summary>
        private static readonly string RootCatalog = @"c:/temp";

        /// <summary>
        /// Starts the server on the defined Defaultport
        /// </summary>
        public void StartServer()
        {
            serverOn = true;

            
            TcpListener goListener = new TcpListener(DefaultPort);
            goListener.Start();

            List<Task> threads = new List<Task>();

            while (serverOn)
            {
                TcpClient goTcpClient = goListener.AcceptTcpClient();
                threads.Add(Task.Run(() => threadHandler(goTcpClient)));
            }
            goListener.Stop();
        }

        /// <summary>
        /// Handles the Threads!
        /// </summary>
        /// <param name="goTcpClient"></param>
        private void threadHandler(TcpClient goTcpClient)
        {
            Console.WriteLine("Server Activated");
            Stream ns = goTcpClient.GetStream();
            StreamReader sr = new StreamReader(ns); // makes a new streamreader in the variable sr
            
            try
            {
                StreamWriter sw = new StreamWriter(ns); // makes a new streamwriter in the variable sw
                sw.AutoFlush = true; //enables automatic flushing
                string message = sr.ReadLine();
                string answer = "";
                string[] words = message.Split(' ');
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }
                string filePath = RootCatalog + words[1];

                
                if (!File.Exists(filePath)) // check if the file does not exist
                {
                    sw.Write("HTTP/1.0 404 Not Found\r\n\r\n");
                    message = sr.ReadLine();
                }
                else
                {
                    answer = "HTTP/1.0 200 OK\r\n\r\n";
                    sw.WriteLine(answer);
                    message = sr.ReadLine();
                    Log.WriteInfo("File requested - Sending: " + filePath); // writes to the log if the file is requested
                    using (FileStream source = File.Open(filePath, FileMode.Open, FileAccess.Read)) // opens the selected file
                    {
                        source.CopyTo(sw.BaseStream);
                        source.Flush();
                    }
                    Console.WriteLine(message);
                    Log.WriteInfo("Server sent the file: " + filePath); // writes to the log if the file is sent

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

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void StopServer()
        {
            serverOn = false;
            Log.WriteInfo("Server shutting down!");
        }
    }
}