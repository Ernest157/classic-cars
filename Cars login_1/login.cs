﻿using System;
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
using System.IO;

namespace Classic_Cars
{
    public partial class Login : Form
    {
        SqlConnection connection = new SqlConnection();

        public Login()
        {
            connection.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\aruns\\Downloads\\CarsDB.mdf;Integrated Security=True;Connect Timeout=30";
            connection.Open();

            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (!((String.IsNullOrEmpty(usernameTB.Text)) || (String.IsNullOrEmpty(passwordTB.Text))))
            {
                String sqlQuery = "SELECT COUNT(*) FROM UserDB WHERE UserName=@USERNAME AND Password=@PASSWORD";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@USERNAME", usernameTB.Text);
                command.Parameters.AddWithValue("@PASSWORD", passwordTB.Text);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);
                if (dataTable.Rows[0][0].ToString() == "1")
                {
                    CatalogScreen catalogScreen = new CatalogScreen();
                    catalogScreen.Show();
                    catalogScreen.LoggedInValue = usernameTB.Text;
                    this.Hide();
                }
            } else
            {
                MessageBox.Show("Please enter a valid Username & Password");
            }
        }

        private void Btn_signup_Click(object sender, EventArgs e)
        { 
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\aruns\\Downloads\\CarsDB.mdf;Integrated Security=True;Connect Timeout=30";
            connection.Open();

            Image img = Image.FromFile(@"C:\Users\aruns\Source\Repos\Ernest157\classic-cars\Cars login_1\Resources\CarIMGs\SUV_7.jpg"); ;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = ms.ToArray();

            String sqlQuery = "UPDATE CarsDB SET Image=@IMAGE WHERE Id=@ID";

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@ID", 16);
            command.Parameters.AddWithValue("@IMAGE", bytes);
            command.ExecuteNonQuery();
        }
    }
}
