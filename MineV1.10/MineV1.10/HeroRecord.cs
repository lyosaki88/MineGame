using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Mine
{
    public partial class HeroRecord : Form
    {
        XmlDocument doc = new XmlDocument();
        public HeroRecord()
        {
            InitializeComponent();
            GetLog();
        }
        private void GetLog() { 
            doc.Load("HeroLog.xml");
            XmlNode xn = doc.ChildNodes[1];
            this.label4.Text = doc.ChildNodes[1].ChildNodes[0].Name;
            this.label5.Text = doc.ChildNodes[1].ChildNodes[1].Name;
            this.label6.Text = doc.ChildNodes[1].ChildNodes[2].Name;
            this.label7.Text = doc.ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText;
            this.label8.Text = doc.ChildNodes[1].ChildNodes[1].ChildNodes[0].InnerText;
            this.label9.Text = doc.ChildNodes[1].ChildNodes[2].ChildNodes[0].InnerText;
            this.label10.Text = doc.ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText;
            this.label11.Text = doc.ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;
            this.label12.Text = doc.ChildNodes[1].ChildNodes[2].ChildNodes[1].InnerText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0;i<3;i++){
                doc.ChildNodes[1].ChildNodes[i].ChildNodes[0].InnerText = "NoName";
                doc.ChildNodes[1].ChildNodes[i].ChildNodes[1].InnerText = "999";
            }
            doc.Save("HeroLog.xml");
            GetLog();
        }
    }
}