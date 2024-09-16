using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;


class TcpEchoServerThread
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 9000);
        ILogger logger = new ConsoleLogger();
        Console.WriteLine("Server listening...");
        listener.Start();
        while (true)
        {
            try
            {
                Socket client = listener.AcceptSocket();
                EchoProtocol protocol = new EchoProtocol(client, logger);
                ThreadStart threadClient = new ThreadStart(protocol.handleClient);
                Thread thread = new Thread(threadClient);
                thread.Start();
                logger.writeEntry("Created and started Thread = " + thread.GetHashCode());
            }
            catch (System.IO.IOException e)
            {
                logger.writeEntry("Exception= " + e.Message);
            }
        }
    }
}
