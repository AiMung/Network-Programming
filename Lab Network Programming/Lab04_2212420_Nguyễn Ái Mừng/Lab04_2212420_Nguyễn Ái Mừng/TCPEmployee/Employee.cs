using System.Text;
using System;



class Employee
{
    public int EmployeeID;
    private int LastNameSize;
    public string LastName;
    private int FirstNameSize;
    public string FirstName;
    public int YearsService;
    public double Salary;
    public int size;

    public Employee()
    {
    }

    public Employee(byte[] data)
    {
        int place = 0;
        EmployeeID = BitConverter.ToInt32(data, place);
        place += 4;
        LastNameSize = BitConverter.ToInt32(data, place);
        place += 4;
        LastName = Encoding.ASCII.GetString(data, place, LastNameSize);
        place = place + LastNameSize;
        FirstNameSize = BitConverter.ToInt32(data, place);
        place += 4;
        FirstName = Encoding.ASCII.GetString(data, place, FirstNameSize);
        place += FirstNameSize;
        YearsService = BitConverter.ToInt32(data, place);
        place += 4;
        Salary = BitConverter.ToDouble(data, place);
    }

    public byte[] GetBytes()
    {
        byte[] data = new byte[1024];
        int place = 0;
        Buffer.BlockCopy(BitConverter.GetBytes(EmployeeID), 0, data, place, 4);
        place += 4;
        Buffer.BlockCopy(BitConverter.GetBytes(LastName.Length), 0, data, place, 4);
        place += 4;
        Buffer.BlockCopy(Encoding.ASCII.GetBytes(LastName), 0, data, place, LastName.Length);
        place += LastName.Length;
        Buffer.BlockCopy(BitConverter.GetBytes(FirstName.Length), 0, data, place, 4);
        place += 4;
        Buffer.BlockCopy(Encoding.ASCII.GetBytes(FirstName), 0, data, place, FirstName.Length);
        place += FirstName.Length;
        Buffer.BlockCopy(BitConverter.GetBytes(YearsService), 0, data, place, 4);
        place += 4;
        Buffer.BlockCopy(BitConverter.GetBytes(Salary), 0, data, place, 8);
        place += 8;
        size = place;
        return data;
    }
}