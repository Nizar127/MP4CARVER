﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using AesEncDec;
using System.IO;

namespace MP4Carver
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        string conn = "datasource=127.0.0.1;port=3306;username=root;password=;database=mp4_carver;";

        public void userLogin()
        {
            string query = "SELECT `name`,`password` FROM users WHERE `name`='" + txtUser.Text + "' AND `password`='" + txtPassword.Text + "' ";
            //connection mysql XAMPP
            MySqlConnection dbconnection = new MySqlConnection(conn);
            MySqlCommand commandDB = new MySqlCommand(query, dbconnection);
            commandDB.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                dbconnection.Open();
                reader = commandDB.ExecuteReader();

                if (txtUser.Text.Length < 3 || txtPassword.Text.Length < 5)
                {
                    MessageBox.Show("Username or Password is invaled or too short!");
                }
                else
                {
                    string dir = txtUser.Text;
                    if (!Directory.Exists("data\\" + dir))
                        MessageBox.Show("User was not found!", dir);
                    else
                    {
                        var sr = new StreamReader("data\\" + dir + "\\data.ls");

                        string encusr = sr.ReadLine();
                        string encpass = sr.ReadLine();
                        sr.Close();

                        string decusr = AesCryp.Decrypt(encusr);
                        string decpass = AesCryp.Decrypt(encpass);

                        if (decusr == txtUser.Text && decpass == txtPassword.Text)
                        {
                            MessageBox.Show("Welcome  to the private area!", decusr);
                            Profile from2 = new Profile();
                            from2.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Error user or password is wrong!");
                        }

                    }
                }


              //  if (reader.HasRows)
               // {
                 //   while (reader.Read())
                   // {
                     //   MessageBox.Show("Login to MP4Carver");
                       // Profile from2 = new Profile();
                       // from2.Show();
                       // this.Hide();
                   // }

                //}
                //else
               // {
                 //   MessageBox.Show("Please try again");
                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userLogin();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }
}
