using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace DBExamples
{
    public partial class Form11 : Form
    {
        DataSet ds;
        SqlDataAdapter da1, da2;
        SqlConnection Con;
        DataRelation dr;
        public Form11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cb=new SqlCommandBuilder(da1);
            SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
            da1.Update(ds,"Dept");
            da2.Update(ds, "Emp");
            MessageBox.Show("Data saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            Con = new SqlConnection(ConStr);
            da1 = new SqlDataAdapter("Select * From Dept", Con);
            da1.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da2 = new SqlDataAdapter("Select * From Emp", Con);
            da2.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            da1.Fill(ds, "Dept");
            da2.Fill(ds, "Emp");

            dr = new DataRelation("ParentChild", ds.Tables["Dept"].Columns["Deptno"], ds.Tables["Emp"].Columns["Deptno"]);
            ds.Relations.Add(dr);
            dr.ChildKeyConstraint.DeleteRule = Rule.None;
            dr.ChildKeyConstraint.UpdateRule = Rule.None;







            dataGridView1.DataSource = ds.Tables["Dept"];
            dataGridView2.DataSource = ds.Tables["Emp"];
            

        }
    }
}
