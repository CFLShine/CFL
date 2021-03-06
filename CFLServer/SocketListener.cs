﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CFLServer
{
    public class SocketListener
    {
        public static void start(int listenPort, Action<StreamWriter, string> handleRequest)
        {
            var server = new TcpListener(IPAddress.Parse("192.168.1.70"), listenPort);
            server.Start();
            Console.WriteLine(" You can connected with Putty on a (RAW session) to {0} to issue JsonRpc requests.", server.LocalEndpoint);
            while (true)
            {
                try
                {
                    using (var client = server.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {
                        Console.WriteLine("Client Connected..");
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var writer = new StreamWriter(stream, new UTF8Encoding(false));

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            handleRequest(writer, line);

                            Console.WriteLine("REQUEST: {0}", line);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("RPCServer exception " + e);
                }
            }
        }
    }
}
