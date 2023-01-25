using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DBExamples
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

           label1.Text= ConfigurationManager.AppSettings.Get("CompanyName");
            label2.Text = ConfigurationManager.AppSettings.Get("Address");
            label3.Text = ConfigurationManager.AppSettings.Get("Phone");
            label4.Text =$"Email : { ConfigurationManager.AppSettings.Get("Email")}   What's App : {ConfigurationManager.AppSettings.Get("WhatsApp")}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
           button2.Text = ConfigurationManager.ConnectionStrings["OracleConStr"].ConnectionString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = ConfigurationManager.ConnectionStrings["ExcelConStr"].ConnectionString;
        }
    }
}
