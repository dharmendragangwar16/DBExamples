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
    public partial class Form14 : Form
    {
        DataSet ds;
        SqlDataAdapter da;

        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlconStr"].ConnectionString;
            da = new SqlDataAdapter("Employee_Select",ConStr);
            ds = new DataSet { };
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];
        }
    }
}
