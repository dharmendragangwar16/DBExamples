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
    public partial class Form7 : Form
    {
        DataSet ds;
        SqlDataAdapter da;

        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            da =new SqlDataAdapter("Select Eno,Ename,Job,Salary from Employee order by Eno",constr);
            ds = new DataSet();
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder sb=new SqlCommandBuilder(da);
            da.Update(ds,"Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];
            MessageBox.Show("Data Saved successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
