using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
	 internal class Program
	 {
		  static void Main(string[] args)
		  {

				int recv;
				byte[] data = new byte[1024];
				IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
				Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				serverSocket.Bind(serverEndPoint);
				Console.WriteLine("Dang cho client ket noi...");
				IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
				EndPoint Remote = (EndPoint)(sender);

				//serverSocket.Connect(Remote);
				recv = serverSocket.ReceiveFrom(data, ref Remote);
				Console.WriteLine("Message received from {0}:", Remote.ToString());
				Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
				string welcome = "UDP Server-Chao mung";
				data = Encoding.ASCII.GetBytes(welcome);
				serverSocket.SendTo(data, data.Length, SocketFlags.None, Remote);
				
				while (true)
				{
					 data = new byte[1024];
					 recv = serverSocket.ReceiveFrom(data, ref Remote);
					 if (Encoding.ASCII.GetString(data, 0, recv) == "exit all")
					 {
						  serverSocket.Close();
						  break;
					 }
					 Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
					 serverSocket.SendTo(data, recv, SocketFlags.None, Remote);
				}
		  }
	 }
}
