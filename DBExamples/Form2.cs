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
using System.Security.Cryptography;

namespace DBExamples
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DKBABA;Database=CSDB; user id=sa; password=1234");
            cmd = new SqlCommand("select Deptno,Dname,Location from Dept order by Deptno", con);

            con.Open();
            dr = cmd.ExecuteReader();
            label1.Text = dr.GetName(0) + " : ";
            label2.Text = dr.GetName(1) + " : ";
            label3.Text = dr.GetName(2 )+ " :" ;
            LoadData();


        }
        private void LoadData()
        {
            if(dr.Read())
            {
                textBox1.Text = dr.GetValue(0).ToString();
                textBox2.Text = dr.GetValue(1).ToString();
                textBox3.Text = dr.GetValue(2).ToString();

            }
            else
            {
                MessageBox.Show("You are at the last recod of table","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(con.State!=ConnectionState.Closed)
            {
                con.Close();

            }
            this.Close();
        }
    }
}
