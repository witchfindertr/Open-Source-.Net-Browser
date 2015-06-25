using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSourceBrowser
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cont3.Checked)
            {
                Properties.Settings.Default.cont = "yes";

            }
            else
            {
                Properties.Settings.Default.cont = "no";
            }
            Console.WriteLine(Properties.Settings.Default.cont);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.homepage = textBox1.Text;
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void s_proxy_Click(object sender, EventArgs e)
        {
            Gecko.GeckoPreferences.User["network.proxy.type"] = 1;
            Gecko.GeckoPreferences.User["network.proxy.http"] = textBox2.Text;
            Gecko.GeckoPreferences.User["network.proxy.http_port"] = int.Parse(textBox5.Text);
            if (checkBox2.Checked)
            {
                Gecko.GeckoPreferences.User["network.proxy.login"] = textBox4.Text;
                Gecko.GeckoPreferences.User["network.proxy.password"] = textBox3.Text;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox4.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
                textBox3.Enabled = false;
            }
        }
    }
}
