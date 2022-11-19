using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searching_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cb_cloumn.SelectedIndex = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            text_id.Text="";
            text_first_name.Text = "";
            text_second_name.Text = "";
            text_search_box.Text = "";



        }

        private void Save_btn_Click(object sender, EventArgs e)
        {

            string Id = text_id.Text;
            string First_name=text_first_name.Text;
            string Second_name=text_second_name.Text;
            

            string connectionString = "Server=DESKTOP-JAHFGFD\\SQLEXPRESS;Database=student_info;Integrated Security=true";

            string query = "INSERT INTO Search_engine Values('" + Id + "','" + First_name + "','" + Second_name + "')";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int row_affected = cmd.ExecuteNonQuery();

            if (row_affected > 0)
            {
                lbl_save.Text = "Saved Sucessfull";
            }
            else
            {
                lbl_save.Text = "Saved Field";
            }

            data_grid_view.Rows.Add(text_id.Text, text_first_name.Text, text_second_name.Text);

        }

        //private void button2_Click(object sender, EventArgs e)
        //{


        //}



        private void text_search_box_TextChanged(object sender, EventArgs e)
        {

            string connectionString = "Server=DESKTOP-JAHFGFD\\SQLEXPRESS;Database=student_info;Integrated Security=true";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string Search_data = text_search_box.Text;
            string Query = "SELECT * FROM Search_engine";

            if (cb_cloumn.SelectedIndex ==0)
            {
                Query += " where First_name like'%" + Search_data + "%' or Second_name like'%" + Search_data + "%'";


                if (int.TryParse(Search_data, out _))
                {
                    Query += " OR Id="+Search_data;
                }

            }
            else
            {
                if (cb_cloumn.SelectedIndex == 1 && Search_data.Length>0)
                {
                    Query += " WHERE Id = "+Search_data;         
                    
                }

                else if (cb_cloumn.SelectedIndex == 2)
                {
                    Query += " WHERE First_name LIKE '%" + Search_data+"%'";
                }
                else if(cb_cloumn.SelectedIndex == 3)
                {
                    Query += " WHERE Second_name LIKE '%" + Search_data + "%'";
                }
            }

            SqlCommand cmd = new SqlCommand(Query, conn);

            var reader = cmd.ExecuteReader();
            data_grid_view.Rows.Clear();

            while (reader.Read())
            {
                data_grid_view.Rows.Add(reader["Id"], reader["First_name"], reader["Second_name"]);
            }
            conn.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-JAHFGFD\\SQLEXPRESS;Database=student_info;Integrated Security=true";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string Search_data = text_search_box.Text;
            string Query = "SELECT * FROM Search_engine where First_name like'%" + Search_data + "%' or Second_name like'%" + Search_data + "%'";
            SqlCommand cmd = new SqlCommand(Query, conn);

            var reader = cmd.ExecuteReader();
            data_grid_view.Rows.Clear();

            while (reader.Read())
            {
                data_grid_view.Rows.Add(reader["Id"], reader["First_name"], reader["Second_name"]);
            }
            conn.Close();
        }
    }
}
