using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quantos_IPs
{
    public partial class Form1 : Form
    {
        bool hidden=false;
        public Form1()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        public string CIDR2DECIMAL(int cidr)
        {
            string[] decim = new string[4];

            // We go through each octagon in the decimal address
            for (int i = 0; i < 4; i++)
            {
                if (cidr > 8)
                {
                    decim[i] = "255";
                    cidr -= 8;
                }
                else
                {
                    int temp = 0;
                    for (int a = 7; cidr > 0; a--, cidr--)
                    {
                        temp += (int)Math.Pow(2, a);
                    }
                    decim[i] = temp.ToString();
                }
            }
            return decim[0] + "." + decim[1] + "." + decim[2] + "." + decim[3];
        }
        public void qualIP()
        {
            if (maskedTextBox1.Text != "")
            {
                try
                {
                    int cidr = Convert.ToInt32(maskedTextBox1.Text);
                    if (cidr < 32 && cidr > 0)
                    {
                        string netmask = CIDR2DECIMAL(cidr);
                        int quantidade = 32 - cidr;
                        MessageBox.Show(" IPs Disponiveis: " + (Math.Pow(2, Convert.ToDouble(quantidade))).ToString() + "\n Hosts Válidos: " + (Math.Pow(2, Convert.ToDouble(quantidade)) - 2).ToString() + "\n Mascara: " + netmask);
                    }
                    else if (cidr == 32)
                    {
                        string netmask1 = CIDR2DECIMAL(cidr);
                        MessageBox.Show("IPs Disponiveis: 1\n Hosts Válidos: 1" + "\n Mascara: " + netmask1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Digite um valor valido!");
                }
            }
            else
            {
                MessageBox.Show("Digite um valor valido!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            qualIP();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void notifyIcon1_DoubleClick_1(object sender, EventArgs e)
        {
            if (hidden)
            {
                this.Show();
                hidden = false;
            }
            else
            {
                this.Hide();
                hidden = true;
            }
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                qualIP();
            }
        }
    }
}
