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

namespace DBExamples
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
             con = new SqlConnection("Data Source=DKBABA;User Id=sa;password=1234;Database=CSDB");
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            LoadData();

        }
        private void LoadData()
        {
            cmd.CommandText = "select Eno,Ename,Job,Salary,Status from Employee where Status=1 order by Eno";
            dr=cmd.ExecuteReader();
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
                checkBox1.Checked = (bool)dr["Status"];
            }
            else
            {
                MessageBox.Show("You are at the last row of the employee table","information",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(Control ctrl in this.Controls)
            {
                if(ctrl is TextBoxBase)
                {
                    TextBoxBase tb = ctrl as TextBoxBase;
                    tb.Clear();
                }
            }
            checkBox1.Checked = false;
            dr.Close();
            cmd.CommandText = "Select IsNull(Max(Eno),1000)+1 from Employee";
            textBox1.Text = cmd.ExecuteScalar().ToString();
            checkBox1.Enabled = true;
            button3.Enabled = true;
            textBox2.Focus();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd.CommandText = $"Insert into Employee (Eno,Ename,Job,Salary,Status) values({textBox1.Text},'{textBox2.Text}','{textBox3.Text}',{textBox4.Text},{Convert.ToInt32(checkBox1.Checked)})";
            if(cmd.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Data inserted successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                LoadData();
                checkBox1.Enabled = false;
                button3.Enabled = false;    
            }
            else
            {
                MessageBox.Show("Failed ,Data is not inserted","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dr.Close();
            cmd.CommandText = $"update Employee set Ename='{textBox2.Text}',Job='{textBox3.Text}',Salary={textBox4.Text} where Eno={textBox1.Text}";
            if(cmd.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Data Updates Successfully", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                LoadData();
                button3.Enabled = false;
                checkBox1.Enabled = false;

            }
            else
            {
                MessageBox.Show("Failed,data is not updated.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure for deleting the current record?","Confirmation",MessageBoxButtons.OK,MessageBoxIcon.Question)==DialogResult.Yes);
            {
                dr.Close();
                cmd.CommandText = $"Update Employee Set Status=0 Where Eno={textBox1.Text}";
                if(cmd.ExecuteNonQuery()>0)
                {
                    MessageBox.Show("Data deleted Successfully.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
                    LoadData(); 
                }
                else
                {
                    MessageBox.Show("Failed data is not deleted.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(con.State!=ConnectionState.Closed)
            {
                con.Close();
            }
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
