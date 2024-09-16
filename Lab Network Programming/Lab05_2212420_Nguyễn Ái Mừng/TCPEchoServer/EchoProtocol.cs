using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class EchoProtocol : IProtocol
{
    public const int BUFSIZE = 32; // Byte size of IO buffer
    private Socket clntSock; // Connection socket
    private ILogger logger; // Logging facility
    public EchoProtocol(Socket clntSock, ILogger logger)
    {
        this.clntSock = clntSock;
        this.logger = logger;
    }

    public void handleClient()
    {
        ArrayList entry = new ArrayList();
        entry.Add("Client address and port = " + clntSock.RemoteEndPoint);
        entry.Add("Thread = " + Thread.CurrentThread.GetHashCode());
        try
        {
            //Receive until client closes connection, indicated by a SocketException
            int recvMsgSize; // Size of received message
            int totalBytesEchoed = 0; // Bytes received from client
            byte[] rcvBuffer = new byte[BUFSIZE]; // Receive buffer
                                                  // Receive untl client closes connection, indicated by 0 return code
            try
            {
                while ((recvMsgSize = clntSock.Receive(rcvBuffer, 0, rcvBuffer.Length,
                SocketFlags.None)) > 0)
                {
                    Console.WriteLine(Encoding.ASCII.GetString(rcvBuffer));
                    clntSock.Send(rcvBuffer, 0, recvMsgSize, SocketFlags.None);
                    totalBytesEchoed += recvMsgSize;
                }
            }
            catch (SocketException se)
            {
                entry.Add(se.ErrorCode + ": " + se.Message);
            }

            entry.Add("Client finished; echoed " + totalBytesEchoed + " bytes.");
        }
        catch (SocketException se)
        {
            entry.Add(se.ErrorCode + ": " + se.Message);
        }
        clntSock.Close();
        logger.writeEntry(entry);
    }
}