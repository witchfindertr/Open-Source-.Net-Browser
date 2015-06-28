using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko;

namespace OpenSourceBrowser
{
    public partial class Form1 : Form
    {
        public bool nav = false;
        public Form1()
        {
            Gecko.Xpcom.Initialize(Environment.CurrentDirectory + "/xulrunner"); 
            InitializeComponent();
        }

        GeckoWebBrowser webBrowser1 = new GeckoWebBrowser();
        int i = 0;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string URL = textBox1.Text;
                e.SuppressKeyPress = true;
                ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(URL);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).GoForward();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).GoBack();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (nav == true) 
            {
                ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Stop();
            }
            else
            {
                ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Refresh();
            }
        }

        private void webBrowser1_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.stop;
            nav = true;
        }

        private void webBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            textBox1.Text = ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Url.ToString();
            tabControl1.SelectedTab.Text = ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle + " | .Net Open Source Browser X";
            pictureBox3.Image = Properties.Resources.refresh;
            nav = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(Properties.Settings.Default.homepage);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1 = new GeckoWebBrowser();
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.Visible = true;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            tabControl1.TabPages.Add("New Tab");
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(webBrowser1);
            i += 1;
            Properties.Settings.Default.Upgrade();
            if (Properties.Settings.Default.cont == "yes")
            {
                webBrowser1.Navigate(Properties.Settings.Default.lastpage);
            }
            else
            {
                webBrowser1.Navigate(Properties.Settings.Default.homepage);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.homepage = webBrowser1.Url.ToString();
            Properties.Settings.Default.Save();
            Console.WriteLine("App Closed");
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Url.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.homepage = webBrowser1.Url.ToString();
            Properties.Settings.Default.Save();
            Console.WriteLine("App Closed");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            webBrowser1 = new GeckoWebBrowser();
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.Visible = true;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            tabControl1.TabPages.Add("New Tab X");
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(webBrowser1);
            textBox1.Text = ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Url.ToString();
            ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(Properties.Settings.Default.homepage);
            i += 1;
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int ia = 0; ia < this.tabControl1.TabPages.Count; ia++)
            {
                Rectangle r = tabControl1.GetTabRect(ia);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    if (tabControl1.TabPages.Count > 1)
                    {
                        this.tabControl1.TabPages.RemoveAt(ia);
                    }
                    else
                    {
                        this.tabControl1.TabPages.RemoveAt(ia);
                        i -= 1;
                        GenerateNewTab();
                    }
                }
            }
        }

        private void GenerateNewTab()
        {
            webBrowser1 = new GeckoWebBrowser();
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.Visible = true;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            tabControl1.TabPages.Add("New Tab");
            //tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(webBrowser1);
            textBox1.Text = ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Url.ToString();
            ((GeckoWebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(Properties.Settings.Default.homepage);
            i += 1;
        }
    }
}
