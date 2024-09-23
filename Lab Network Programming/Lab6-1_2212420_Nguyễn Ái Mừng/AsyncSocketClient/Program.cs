using System;
using AsyncSocketTCP;

namespace AsyncSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncSocketTCPClient client = new AsyncSocketTCPClient();

            Console.WriteLine("*** Welcome to Async Socket Client ***");
            Console.Write("Enter Server IP Address: ");
            string strIPAddress = Console.ReadLine();

            Console.Write("Enter Port Number (8 - 65535): ");
            string strPortInput = Console.ReadLine();

            if (!client.SetServerIPAddress(strIPAddress) ||
                !client.SetPortNumber(strPortInput))
            {
                Console.WriteLine(string.Format(

                    "IP Address or port number invalid [{0}] [{1}] Press a key to exit",
                    strIPAddress, strPortInput));

                Console.ReadKey();
                return;
            }

            client.ConnectToServer();

            string strInputUser = null;

            do
            {
                strInputUser = Console.ReadLine();

                if (strInputUser.Trim() != "<EXIT>")
                {
                    client.SendToServer(strInputUser);
                }
                else if (strInputUser.Equals("<EXIT>"))
                {
                    client.CloseAndDisconnect();
                }
            } while (strInputUser != "<EXIT>");

        }
    }
}
