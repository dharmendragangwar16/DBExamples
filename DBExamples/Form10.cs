using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;

namespace DBExamples
{
    public partial class Form10 : Form
    {
        DataSet ds;
        SqlDataAdapter sda;
        OleDbDataAdapter oda;
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            string SqlConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            string ExcelConStr = ConfigurationManager.ConnectionStrings["ExcelConStr"].ConnectionString;
            sda = new SqlDataAdapter("Select * from Employee", SqlConStr);
            oda = new OleDbDataAdapter("Select * from Student", ExcelConStr);


            ds = new DataSet();
            sda.Fill(ds,"Employee");
            oda.Fill(ds,"Student");

            dataGridView1.DataSource = ds.Tables["Student"];
            dataGridView2.DataSource = ds.Tables["Employee"];


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cb = new SqlCommandBuilder();
            sda.Update(ds,"Employee");
            MessageBox.Show("Data Saved successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
