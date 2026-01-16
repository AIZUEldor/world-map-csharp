using System;
using System.Windows.Forms;

namespace WorldMap
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            new MountainsForm().ShowDialog();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCountries_Click_1(object sender, EventArgs e)
        {
            new CountriesForm().ShowDialog();
        }

        private void btnCities_Click_1(object sender, EventArgs e)
        {
            new CitiesForm().ShowDialog();
        }

        private void btnRegions_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Regions form hali yozilmagan 🙂");
        }

        private void btnRivers_Click_1(object sender, EventArgs e)
        {
            new RiversForm().ShowDialog();
        }

        private void btnLakes_Click_1(object sender, EventArgs e)
        {
            new LakesForm().ShowDialog();
        }

        private void btnSeas_Click_1(object sender, EventArgs e)
        {
            new SeasForm().ShowDialog();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }
    }
}
