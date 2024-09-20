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

namespace AsyncSocketServer
{
    public partial class FrmServer : Form
    {
      
        public FrmServer()
        {
            InitializeComponent();
            
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            btnAccept.BackColor = Color.Green;
        }
    }
}
