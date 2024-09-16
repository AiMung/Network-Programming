using System.Net.Sockets;
using System.Net;
using System;
using System.Text;




class EmployeeClient
{   
    public static void Main()
    {
 
        Employee emp1 = new Employee();
        Employee emp2 = new Employee();
        TcpClient client;

        emp1.EmployeeID = 1;
        emp1.LastName = "Nguyen";
        emp1.FirstName = "Ai Mung";
        emp1.YearsService = 4;
        emp1.Salary = 35000.50;

        emp2.EmployeeID = 2;
        emp2.LastName = "Mung";
        emp2.FirstName = "Nguyen Ai";
        emp2.YearsService = 9;
        emp2.Salary = 23700.30;

        try
        {
            client = new TcpClient("127.0.0.1", 9050);
        }
        catch (SocketException)
        {
            Console.WriteLine("Không kết nối với server được, thử lại sau");
            return;
        }
        NetworkStream ns = client.GetStream();

        byte[] data = emp1.GetBytes();
        int size = emp1.size;
        byte[] packsize = new byte[2];
        Console.WriteLine("packet size = {0}", size);
        packsize = BitConverter.GetBytes(size);
        ns.Write(packsize, 0, 2);
        ns.Write(data, 0, size);
        ns.Flush();

        data = emp2.GetBytes();
        size = emp2.size;
        packsize = new byte[2];
        Console.WriteLine("packet size = {0}", size);
        packsize = BitConverter.GetBytes(size);
        ns.Write(packsize, 0, 2);
        ns.Write(data, 0, size);
        ns.Flush();

        ns.Close();
        client.Close();
       Console.ReadKey();
    }
}