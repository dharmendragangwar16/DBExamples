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
    public partial class Form16 : Form
    {
        DataSet ds;
        SqlDataAdapter da;

        public Form16()
        {
            InitializeComponent();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            da = new SqlDataAdapter("Employee_Select", ConStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            da.SelectCommand.Parameters.Clear();
            if(comboBox1.SelectedIndex==1)
            {
                da.SelectCommand.Parameters.Add("@Status", true);
            }
            else if(comboBox1.SelectedIndex==2)
            {
                da.SelectCommand.Parameters.Add("@Status", false);
            }
            ds = new DataSet();
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];

        }
    }
}
