﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Any;

            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);
            try
            {
                listenerSocket.Bind(ipep);

                listenerSocket.Listen(5);

                Console.WriteLine("About to accept incoming connection.");

                Socket client = listenerSocket.Accept();

                Console.WriteLine("Client connected. " + client.ToString() + " - IP End Point: " + client.RemoteEndPoint.ToString());

                byte[] buff = new byte[128];
                int numberOfReceiveBytes = 0;
                while (true)
                {
                    numberOfReceiveBytes = client.Receive(buff);
                    Console.WriteLine("number of received bytes: " + numberOfReceiveBytes);

                    Console.WriteLine("data sent by client is :" + buff);

                    string receivedText = Encoding.ASCII.GetString(buff, 0, numberOfReceiveBytes);


                    Console.WriteLine("Data sent by client is: " + receivedText);
                    // Console.ReadKey();

                    client.Send(buff);
                    if (receivedText == "x")
                    {
                        break;
                    }
                    Array.Clear(buff, 0, buff.Length);
                    numberOfReceiveBytes = 0;


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
              //  throw;
            }

            

        }
    }
}
