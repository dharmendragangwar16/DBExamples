using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DBExamples
{
    public partial class Form4 : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=D:\\ameerpet study\\dotnet\\Excel\\School.xls;Extended properties=Excel 8.0");
            cmd = new OleDbCommand();
            cmd.Connection= con;
            con.Open();
            LoadData();
            ShowData(); 

        }
        private void LoadData()
        {
            cmd.CommandText = "Select * From [Student$]";
            dr = cmd.ExecuteReader();
            ShowData();
        }
        private void ShowData()
        {
            if (dr.Read())
            {
                textBox1.Text = dr["Sno"].ToString();
                textBox2.Text = dr["Sname"].ToString();
                textBox3.Text = dr["Class"].ToString();
                textBox4.Text = dr["Fees"].ToString();
                btnInsert.Enabled = false;
            }
            else
            {
                MessageBox.Show("You are at the last row of the table","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox1.Focus();
           
            textBox1.ReadOnly = false;
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(con.State !=ConnectionState.Closed)
            {
                con.Close();
            }
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dr.Close();
            cmd.CommandText = $"update [Student$] set Sname='{textBox2.Text}',Class={textBox3.Text},Fees={textBox4.Text} where Sno={textBox1.Text}";
            
            
            
            if(cmd.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Data Inserted successfully.","Seccess",MessageBoxButtons.OK,MessageBoxIcon.Information);
                LoadData();
                ShowData();
            }
            else
            {
                MessageBox.Show("Failed data is not inserted","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            


        }
       

        private void btnInsert_Click(object sender, EventArgs e)
        {
            dr.Close();
            textBox1.ReadOnly = false;
            cmd.CommandText = $"INSERT INTO [STUDENT$] (SNO,SNAME,CLASS,FEES) VALUES({textBox1.Text},'{textBox2.Text}',{textBox3.Text},{textBox4.Text})";
            if (textBox1.TextLength>0 && textBox2.TextLength>0 && textBox3.TextLength>0 && textBox4.TextLength>0)
            {
                
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Data Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnInsert.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed data is not inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the Fields.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
           
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBox4.Text.Length>0)
            {
                btnInsert.Enabled = true;
            }
            else
            {
                btnInsert.Enabled = false;
            }
        }

       
    }
}
