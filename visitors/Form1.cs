using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace visitors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
        }

        MySqlConnection con = new MySqlConnection("Server=localhost;Database=visitorsDB;Uid=root;Pwd=");
        MySqlCommand cmd;
        MySqlDataReader read;
        string id;
        bool Mode = true;
 
        string sql;


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string visitorName = textBox1.Text;
            string visitorContactNumber = textBox2.Text;
            string reason = richTextBox1.Text;


            if (textBox1.Text == "" )
            {
                MessageBox.Show("Please Enter Visitor Name!!");
                textBox1.Focus();
            }
            else if(textBox2.Text == "")
            {
                MessageBox.Show("Please Enter Visitor Contact Number!!");
                textBox2.Focus();
            }
            else if(richTextBox1.Text == "")
            {
                MessageBox.Show("Please Enter Reason!");
                richTextBox1.Focus();
            }
            else
            {
                if (Mode == true)
                {
                    sql = "insert into visitorstb(visitorName,visitorContactNumber,reason) values(@visitorName,@visitorContactNumber,@reason)";
                    con.Open();
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@visitorName", visitorName);
                    cmd.Parameters.AddWithValue("@visitorContactNumber", visitorContactNumber);
                    cmd.Parameters.AddWithValue("@reason", reason);
                    MessageBox.Show("Record Added Successfully");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    textBox1.Clear();
                    textBox2.Clear();
                    richTextBox1.Clear();
                    textBox1.Focus();
                    load();

                }
                else
                {
                    id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    sql = "UPDATE visitorstb SET visitorName = visitorName,visitorContactNumber = @visitorContactNumber , reason = @reason where id = @id  ";
                    con.Open();
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@visitorName", visitorName);
                    cmd.Parameters.AddWithValue("@visitorContactNumber", visitorContactNumber);
                    cmd.Parameters.AddWithValue("@reason", reason);
                    cmd.Parameters.AddWithValue("@id", id);
                    MessageBox.Show("Record Updated Successfully");
                    cmd.ExecuteNonQuery();
                    con.Close();

                    textBox1.Clear();
                    textBox2.Clear();
                    richTextBox1.Clear();
                    textBox1.Focus();
                    button1.Text = "Save";
                    Mode = true;
                    load();
                }
                con.Close();
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >=0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = " delete from visitorstb where id = @id";
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
                load();

            }
        }

        public void load()
        {
            try
            {
                sql = "select * from visitorstb";
                cmd = new MySqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3], read[4]);
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getID(string id)
        {
            sql = "select * from visitorstb where id = '" + id + "'";

            cmd = new MySqlCommand(sql, con);
            con.Open();

            read = cmd.ExecuteReader();
            Console.WriteLine(read);
            while(read.Read())
            {
                textBox1.Text = read[1].ToString();
                textBox2.Text = read[2].ToString();
                richTextBox1.Text = read[3].ToString();
                
            }
            con.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            textBox1.Focus();
        }

      
        private void timer2_Tick(object sender, EventArgs e)
        {
            load();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            load();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
