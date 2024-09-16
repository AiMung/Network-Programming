using System.Net.Sockets;
using System;
using System.Net;
using System.Text;

class EmployeeSrvr
{ 
    public static void Main()
    {
      
        byte[] data = new byte[1024];
        TcpListener server = new TcpListener(9050);

        server.Start();
        TcpClient client = server.AcceptTcpClient();
        NetworkStream ns = client.GetStream();

        byte[] size = new byte[2];
        int recv = ns.Read(size, 0, 2);
        int packsize = BitConverter.ToInt16(size, 0);
        Console.WriteLine("packsize = {0}", packsize);
        recv = ns.Read(data, 0, packsize);

        Employee emp1 = new Employee(data);
        Console.WriteLine("emp1.EmployeeID = {0}", emp1.EmployeeID);
        Console.WriteLine("emp1.LastName = {0}", emp1.LastName);
        Console.WriteLine("emp1.FirstName = {0}", emp1.FirstName);
        Console.WriteLine("emp1.YearsService = {0}", emp1.YearsService);
        Console.WriteLine("emp1.Salary = {0}\n", emp1.Salary);

        size = new byte[2];
        recv = ns.Read(size, 0, 2);
        packsize = BitConverter.ToInt16(size, 0);
        data = new byte[packsize];
        Console.WriteLine("packsize = {0}", packsize);
        recv = ns.Read(data, 0, packsize);

        Employee emp2 = new Employee(data);
        Console.WriteLine("emp2.EmployeeID = {0}", emp2.EmployeeID);
        Console.WriteLine("emp2.LastName = {0}", emp2.LastName);
        Console.WriteLine("emp2.FirstName = {0}", emp2.FirstName);
        Console.WriteLine("emp2.YearsService = {0}", emp2.YearsService);
        Console.WriteLine("emp2.Salary = {0}", emp2.Salary);

        ns.Close();
        client.Close();
        server.Stop();
        Console.ReadKey();

    }
}