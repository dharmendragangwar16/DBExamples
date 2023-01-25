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
    public partial class Form8 : Form
    {
        bool flag = false;
        DataSet ds;
        SqlDataAdapter da;
        
        public Form8()
        {
            InitializeComponent();
        }

       

        private void Form8_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            da = new SqlDataAdapter("Select * from Dept",ConStr);
            ds = new DataSet();
            da.Fill(ds,"Dept");
            da.SelectCommand.CommandText = "Select * from Emp";
            da.Fill(ds, "Emp");
            comboBox1.DataSource = ds.Tables["Dept"];
            comboBox1.DisplayMember = "Dname";
            comboBox1.ValueMember = "Deptno";
            comboBox1.Text = "Select Department";
            dataGridView1.DataSource = ds.Tables["Emp"];
            flag = true;
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(flag)
            {
                DataView dv = ds.Tables["Emp"].DefaultView;
                dv.RowFilter = "Deptno =" + comboBox1.SelectedValue;
                dv.Sort = "salary desc,comm desc";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(da);
            da.Update(ds, "Dept");
            dataGridView1.DataSource = ds.Tables["Dept"];
            MessageBox.Show("Data Saved successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
