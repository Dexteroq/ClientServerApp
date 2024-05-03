using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient tcpClient;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpClient = new SimpleTcpClient();
            tcpClient.StringEncoder = Encoding.UTF8;
            tcpClient.DataReceived += TcpClient_DataReceived;
        }

        private void TcpClient_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.AppendText(e.MessageString + Environment.NewLine);

            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            tcpClient.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
            tcpClient.WriteLineAndGetReply(txtMessage.Text, TimeSpan.FromSeconds(3));
        }
    }
}
