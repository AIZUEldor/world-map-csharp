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
using WorldMap.Helpers;
using WorldMap.Services;

namespace WorldMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
 
        }
        

        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }

       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text.Trim();
                string parol = textBox2.Text;

                if (AuthService.Login(login, parol))
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Login yoki parol xato ❌",
                        "Xatolik",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }

        }
    }
}
