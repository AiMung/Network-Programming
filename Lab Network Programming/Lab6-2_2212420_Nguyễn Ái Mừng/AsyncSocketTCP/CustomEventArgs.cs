using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocketTCP
{
    public  class CustomEventArgs
    {
        public class ClientConnectedEventArgs : EventArgs
        {
            public string NewClient { get; set; }
            public ClientConnectedEventArgs(string _newClient)
            { NewClient = _newClient; }
        }
    }
}
