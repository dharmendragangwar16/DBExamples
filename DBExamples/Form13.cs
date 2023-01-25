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


namespace DBExamples
{
    public partial class Form13 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form13()
        {

            InitializeComponent();
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            con = new SqlConnection(ConStr);
            cmd = new SqlCommand("Employee_select", con);
            con.Open();
            dr= cmd.ExecuteReader();
            ShowData();

        }
        private void ShowData()
        {
            if(dr.Read())
            {
                textBox1.Text = dr["Eno"].ToString();
                textBox2.Text = dr["Ename"].ToString();
                textBox3.Text = dr["Job"].ToString();
                textBox4.Text = dr["Salary"].ToString();
            }
            else
            {
                MessageBox.Show("You are at the last record  of the table.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowData(); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(con.State !=ConnectionState.Closed)
            {
                con.Close();

            }
            this.Close();
        }
    }
}
