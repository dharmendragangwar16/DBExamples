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
using System.IO;
using static Microsoft.VisualBasic.Interaction;

namespace DBExamples
{
    public partial class Form15 : Form
    {
        string imgpath;
        byte[] imgdata = null;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form15()
        {
            InitializeComponent();
        }
        private void Form15_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            con=new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            pictureBox1.Image = null;
            imgpath = null;
            imgdata = null;
            textBox2.Focus();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnCloseImage_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            imgdata = null;
            imgpath = "";

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            if(btnInsert.Text=="New")
            {
                btnInsert.PerformClick();
                btnInsert.Text = "Insert";
            }
            else
            {
                try
                {
                    cmd.CommandText = "Employee_Insert";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Ename", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Job", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Salary", textBox4.Text);
                    if(imgpath.Trim().Length>0)
                    {
                        imgdata=File.ReadAllBytes(imgpath);
                        cmd.Parameters.AddWithValue("@Photo", imgdata);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                        cmd.Parameters["@Photo"].SqlDbType = SqlDbType.VarBinary;
                    }
                    cmd.Parameters.Add("@Eno",SqlDbType.Int,4).Direction=ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    textBox1.Text = cmd.Parameters["@Eno"].Value.ToString();
                    imgdata = null;
                    imgpath = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                    btnInsert.Text = "New";

                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string Value = InputBox("Enter Employee Number to search :");
            if(int.TryParse(Value,out int Eno))
            {
                try
                {
                    cmd.CommandText = "Employee_select";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Eno", Eno);
                    cmd.Parameters.AddWithValue("@Status", true);
                    con.Open();
                    dr=cmd.ExecuteReader(); 
                    if(dr.Read())
                    {
                        textBox1.Text = dr["Eno"].ToString();
                        textBox2.Text = dr["Ename"].ToString();
                        textBox3.Text = dr["job"].ToString();
                        textBox4.Text = dr["Salary"].ToString();

                        if (dr["Photo"]!=DBNull.Value)
                        {
                            imgdata = (byte[])dr["Photo"];
                            MemoryStream ms = new MemoryStream(imgdata);
                            pictureBox1.Image = Image.FromStream(ms);

                        }
                        else
                        {
                            imgdata = null;
                            imgpath = "";
                            pictureBox1.Image = null;

                        }
                    }
                    else
                    {
                        MessageBox.Show("No employee Exixts with given employee number","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    con.Close();
                }
               
            }
            else
            {
                MessageBox.Show("Employee number should be integer only.", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg Images|*.jpg|Bitmap Images|*.bmp|Png Images|*.png|All Files|*.*";
            DialogResult dr = openFileDialog1.ShowDialog();
            if(dr==DialogResult.OK)
            {
                imgpath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = imgpath;

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "Employee_Update";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Eno", textBox1.Text);
                cmd.Parameters.AddWithValue("Ename", textBox2.Text);
                cmd.Parameters.AddWithValue("@Job", textBox3.Text);
                cmd.Parameters.AddWithValue("Salary", textBox4.Text);

                if (imgdata == null && imgpath.Trim().Length == 0)
                {
                    cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                    cmd.Parameters["@Photo"].SqlDbType = SqlDbType.VarBinary;
                }
                else if(imgpath.Trim().Length>0)  
                {
                    imgdata=File.ReadAllBytes(imgpath);
                    cmd.Parameters.AddWithValue("@Photo",imgdata);
                }
                else if(imgpath.Trim().Length==0 && imgdata !=null)
                {
                    cmd.Parameters.AddWithValue("@Photo",imgdata);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data updated succesfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "Employee_Delete";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Eno",textBox1.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                btnClear.PerformClick();
                MessageBox.Show("Record deleted successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
