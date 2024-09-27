using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using AsyncSocketTCP;

namespace AsyncSocketServer
{
    public partial class FrmServer : Form
    {
        AsyncSocketTCPServer mServer;

        public FrmServer()
        {
            InitializeComponent();
            mServer = new AsyncSocketTCPServer();
            mServer.ClientConnectedEvent += HandleClientConnected;

            
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            btnAccept.BackColor = Color.Green;
            mServer.StartListenningForIncomingConnection();
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(txtMessage.Text.Trim());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mServer.StopServer();

        }

        private void FrmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }
        void HandleClientConnected(object sender, ClientConnectedEventArgs e)
        {
            txtClientInfo.AppendText(string.Format("{0} - New Client Connected - {1}\r\n", DateTime.Now, e.NewClient));
        }
    }
}
