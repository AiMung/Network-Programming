using System;
using System.Collections;
using System.Threading;


class ConsoleLogger : ILogger
{
    private static Mutex mutex = new Mutex();

    public void writeEntry(ArrayList entry)
    {
        mutex.WaitOne();
        IEnumerator lines = entry.GetEnumerator();
        while (lines.MoveNext())
            Console.WriteLine(lines.Current);
        Console.WriteLine();
        mutex.ReleaseMutex();
    }
    public void writeEntry(String entry)
    {
        mutex.WaitOne();
        Console.WriteLine(entry);
        Console.WriteLine();
        mutex.ReleaseMutex();
    }
}