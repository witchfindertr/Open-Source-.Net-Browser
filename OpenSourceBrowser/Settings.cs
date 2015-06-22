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
    }
}
