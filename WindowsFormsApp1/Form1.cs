using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer tcpServer;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpServer = new SimpleTcpServer();
            tcpServer.Delimiter = 0x13; //enter
            tcpServer.StringEncoder = Encoding.UTF8;
            tcpServer.DataReceived += TcpServer_DataReceived;
        }

        private void TcpServer_DataReceived(object sender, SimpleTCP.Message e)
        {
            
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                if (!string.IsNullOrEmpty(txtStatus.Text))
                    txtStatus.AppendText(Environment.NewLine); 
                txtStatus.AppendText("Client said:"); 
                txtStatus.AppendText(e.MessageString + Environment.NewLine); 
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server Starting...";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            tcpServer.Start(ip,Convert.ToInt32(txtPort.Text));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (tcpServer.IsStarted) 
            tcpServer.Stop();            
        }
    }
}
