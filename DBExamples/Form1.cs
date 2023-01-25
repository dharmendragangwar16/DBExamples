using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OdbcConnection conn = new OdbcConnection("DSN=SqlDSN");
            conn.Open();
            MessageBox.Show("Connection is " + conn.State, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            MessageBox.Show("connection is " + conn.State, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("Provider=SqlOledb; data source=dkbaba;user id=sa;password=1234;");
            conn.Open();
            MessageBox.Show("connection is " + conn.State, "inforation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            MessageBox.Show("connection is " + conn.State, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("data source=dkbaba;integrated security=SSPI");
            conn.Open();
            MessageBox.Show("connection is " + conn.State, "inforation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            MessageBox.Show("connection is " + conn.State, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OdbcConnection conn = new OdbcConnection("DSN=ExcelDSN");
            conn.Open();
            MessageBox.Show("connection is " + conn.State, "inforation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            MessageBox.Show("connection is " + conn.State, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=D:\\ameerpet study\\dotnet\\Excel\\Student.xls;Extended properties=Excel 8.0");

            conn.Open();
            MessageBox.Show("connection is " + conn.State, "inforation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            MessageBox.Show("connection is " + conn.State, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
