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
using System.Data;
using static Microsoft.VisualBasic.Interaction;

namespace DBExamples
{
    public partial class Form5 : Form
    {
        SqlDataAdapter da;
        DataSet ds;
        int RowIndex=0;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("SELECT ENO,ENAME,JOB,SALARY FROM EMPLOYEE","Data Source=DKBABA;Database=CSDB;User Id=sa;Password=1234");
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
            ShowData();
            
        }
        private void ShowData()
        {
            btnInsert.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            if (ds.Tables["Employee"].Rows[RowIndex].RowState!=DataRowState.Deleted)
            {
                textBox1.Text = ds.Tables["Employee"].Rows[RowIndex]["Eno"].ToString();
                textBox2.Text = ds.Tables["Employee"].Rows[RowIndex]["Ename"].ToString();
                textBox3.Text = ds.Tables["Employee"].Rows[RowIndex]["Job"].ToString();
                textBox4.Text = ds.Tables["Employee"].Rows[RowIndex]["Salary"].ToString();

            }
            else
            {
                MessageBox.Show("Current row is deleted and can't be accessed anymore","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            RowIndex = 0;
            ShowData();
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if(RowIndex>0)
            {
                RowIndex -= 1;
                ShowData();
            }
            else
            {
                MessageBox.Show("You are at the first row at the table","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
           
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (RowIndex < ds.Tables[0].Rows.Count-1)
            {
                RowIndex += 1;
                ShowData();
            }
            else
            {
                MessageBox.Show("you are at the last row of the table","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
           
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            RowIndex=ds.Tables[0].Rows.Count-1;
            ShowData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            btnInsert.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            int LastRowIndex=ds.Tables[0].Rows.Count-1;

            int MaxEno =Convert.ToInt32( ds.Tables["Employee"].Rows[LastRowIndex]["Eno"]);
            
            textBox1.Text = (1 + MaxEno).ToString();

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0)
            {

                DataRow dr = ds.Tables["employee"].NewRow();
                dr["Eno"] = textBox1.Text;
                dr["Ename"] = textBox2.Text;
                dr["Job"] = textBox3.Text;
                dr["Salary"] = textBox4.Text;

                ds.Tables["Employee"].Rows.Add(dr);
                //setting the rowindex to he index of addin new row
                RowIndex = ds.Tables[0].Rows.Count - 1;
                btnInsert.Enabled = false;
                MessageBox.Show("Row inserted into data set", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Plsease fill all the fields","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ds.Tables["Employee"].Rows[RowIndex]["Ename"] = textBox2.Text;
            ds.Tables["Employee"].Rows[RowIndex]["Job"] = textBox3.Text;
            ds.Tables["Employee"].Rows[RowIndex]["Salary"] = textBox4.Text;

            MessageBox.Show("Data updated Successfully in data set","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds.Tables["Employee"].Rows[RowIndex].Delete();
            btnFirst.PerformClick();
            MessageBox.Show("Data deleted from the the data Set","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(da);
            da.Update(ds,"Employee");

            btnFirst.PerformClick();
            MessageBox.Show("Data saved successfully in Database","Success",MessageBoxButtons.OK,MessageBoxIcon.Question);



        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Value = InputBox("Enter Employee No :");
            if(int.TryParse(Value,out int Eno))
            {
                DataRow dr = ds.Tables[0].Rows.Find(Eno);
                if(dr!= null)
                {
                    RowIndex =ds.Tables[0].Rows.IndexOf(dr);
                    textBox1.Text = dr["Eno"].ToString();
                    textBox2.Text = dr["Ename"].ToString();
                    textBox3.Text = dr["job"].ToString();
                    textBox4.Text = dr["Salary"].ToString();

                }
                else
                {
                    MessageBox.Show("No Result found of given employee no.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Employee number must be an Integer Value","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
               
        }
    }
}
