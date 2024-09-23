using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;


namespace AsyncSocketTCP
{
    public class AsyncSocketTCPClient
    {
        public IPAddress mServerIPAddress;
        public int mServerPort;
        public TcpClient mClient;

        public IPAddress ServerIPAddress
        {
            get
            {
                return mServerIPAddress;
            }
        }
        public int ServerPort
        {
            get
            {
                return mServerPort;
            }
        }
        //phương thức khởi tạo
        public AsyncSocketTCPClient()
        {
            mClient = null;
            mServerPort = -1;
            mServerIPAddress = null;

        }
        //phương thức thiết đặt địa chỉ IP của server
        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ipaddr = null;
            if(!IPAddress.TryParse(_IPAddressServer,out ipaddr))
            {
                Console.WriteLine("Invalid IP address");
                return false;
            }
            mServerIPAddress = ipaddr;
            return true;
        }
        //phương thức thiết đặt Port Number
        public bool SetPortNumber(string _ServerPort)
        {
            int portNumber = 0;
            if( !int.TryParse(_ServerPort.Trim(),out portNumber))
            {
                Console.WriteLine("Invalid port number");
                return false;
            }
            if(portNumber<=0|| portNumber>65535)
            {
                Console.WriteLine("Port number must be between 0 and 65535.");
                return false;
            }
            mServerPort = portNumber;
            return true;

        }
        //phương thức đóng kết nối
        public void CloseAndDisconnect()
        {
            if(mClient!=null)
            {
                if(mClient.Connected)
                {
                    mClient.Close();
                }    
            }    
        }
        //phương thức bất đồng bộ kết nối đến server
        public async Task ConnectToServer()
        {
            if(mClient==null)
            {
                mClient = new TcpClient();
            }
            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                Console.WriteLine(string.Format("Connected to server IP/Port :{0} / {1}",mServerIPAddress,mServerPort));
                await ReadDataAsync(mClient);

            }
            catch(Exception excp)
            {
                Console.WriteLine(excp.ToString());
                throw;
            }
        }
        //phương thức bất đồng bộ gửi dữ liệu đến server
        public async Task SendToServer(string strInputUser)
        {
            if (string.IsNullOrEmpty(strInputUser))
            {
                Console.WriteLine("Empty message, no data send.");
                return;
            }    
            if(mClient !=null)
            {
                if(mClient.Connected)
                {
                    StreamWriter clientStreamWriter = new StreamWriter(mClient.GetStream());
                    clientStreamWriter.AutoFlush = true;
                    await clientStreamWriter.WriteAsync(strInputUser);
                    Console.WriteLine("Data sent ...");
                }    
            }    
        }
        //Phương bất đồng bộ Nhận dữ liệu từ Server
        private async Task ReadDataAsync(TcpClient tcpClient)
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(mClient.GetStream());
                char[] buff = new char[64];
                int readByteCount = 0;
                while(true)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);
                    if(readByteCount<=0)
                    {
                        Console.WriteLine("Disconnected from Server.");
                        mClient.Close();
                        break;
                    }
                    Console.WriteLine(string.Format("Received bytes: {0} - message: {1}",readByteCount,new string(buff)));
                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch(Exception excp)
            {
                Console.WriteLine(excp.ToString());
                throw;
            }
        }

    }

}
