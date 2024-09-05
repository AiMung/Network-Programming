using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
	 internal class Program
	 {
		  public static void Main()
		  {
				try
				{
					 byte[] data = new byte[1024];
					 string input, str;
					 IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
					 Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					 server.EnableBroadcast = true;
					 string welcome = "Xin chao UDP Server";
					 data = Encoding.ASCII.GetBytes(welcome);
					 server.SendTo(data, data.Length, SocketFlags.None, ipep);
					 IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
					 EndPoint Remote = (EndPoint)sender;
					 data = new byte[1024];
					 int recv = server.ReceiveFrom(data, ref Remote);
					 Console.WriteLine("Message received from {0}:", Remote.ToString());
					 Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
					 while (true)
					 {
						  input = Console.ReadLine();
						  if (input == "exit all")
						  {
								server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
								server.Close();
								break;
						  }
						  if (input == "exit")
						  {
								string exit = "Client stopped";
								data = Encoding.ASCII.GetBytes(exit);
								server.SendTo(data, data.Length, SocketFlags.None, ipep);
								break;
						  }
						  server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
						  data = new byte[1024];
						  recv = server.ReceiveFrom(data, ref Remote);
						  str = Encoding.ASCII.GetString(data, 0, recv);
						  Console.WriteLine(str);

					 }
				}
				catch (SocketException ex)
				{
					 Console.WriteLine("Server chua khoi dong");
				}
				Console.ReadKey();
		  }
	 }
}


