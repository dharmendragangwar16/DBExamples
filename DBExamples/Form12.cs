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
    public partial class Form12 : Form
    {
        DataSet ds;
        SqlDataAdapter da1, da2, da3;
        SqlConnection Con;
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            Con = new SqlConnection(ConStr);
            da1 = new SqlDataAdapter("Select * from Dept", Con);
            da2 = new SqlDataAdapter("Select * From Emp", Con);
            da3 = new SqlDataAdapter("Select * from Salgrade", Con);

            ds = new DataSet();
            da1.Fill(ds, "Dept");
            da2.Fill(ds, "Emp");
            da3.Fill(ds, "Salgrade");

            ds.Relations.Add("Parentchild", ds.Tables["Dept"].Columns["Deptno"], ds.Tables["Emp"].Columns["Deptno"]);
            dataGrid1.DataSource= ds;


           
        }
    }
}
