using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = null;

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = null;

            try
            {
                Console.WriteLine("**** Welcome to Socket Client Starter Example by Tapos Ghosh");
                Console.WriteLine("**** Please Type a Valid Server IP Address and Press Enter:");
                string strIPAddress = Console.ReadLine();

                Console.WriteLine("Please Supply a Valid Port Number 0-65535 and Press Enter: ");

                string strPortInput = Console.ReadLine();

                int nPortNumber = 0;



                if (!IPAddress.TryParse(strIPAddress, out ipaddr))
                {
                    Console.WriteLine("Invalid server IP supplied,return.");
                    return;
                }

                if (!int.TryParse(strPortInput.Trim(), out nPortNumber))
                {
                    Console.WriteLine("Invalid port number supplied,return.");
                    return;
                }

                if (nPortNumber <= 0 || nPortNumber > 65535)
                {
                    Console.WriteLine("Port Number must be between 0-65535");
                }

                Console.WriteLine(string.Format("IPAddress:{0} - Port:{1}", ipaddr.ToString(), nPortNumber));

                client.Connect(ipaddr, nPortNumber);

                Console.WriteLine(
                    "Connected to the server, type text and press enter to send it to the server, type <EXIT> to close");

                string inputCommadn = String.Empty;

                while (true)
                {
                    inputCommadn = Console.ReadLine();
                    if (inputCommadn.Equals("<EXIT>"))
                    {
                        break;
                    }

                    byte[] buffSend = Encoding.ASCII.GetBytes(inputCommadn);
                    client.Send(buffSend);

                    byte[] buffReceived = new byte[128];
                    int nRecv = client.Receive(buffReceived);

                    Console.WriteLine("Data received: {0}", Encoding.ASCII.GetString(buffReceived, 0, nRecv));


                }

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        client.Dispose();
                    }
                    
                }
                
            }

            Console.WriteLine("Press a key to exit ......");
            Console.ReadKey();
        }
    }
}
