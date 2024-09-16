using System.Collections;
using System;



public interface ILogger
{
    void writeEntry(ArrayList entry);
    void writeEntry(String entry);
}

