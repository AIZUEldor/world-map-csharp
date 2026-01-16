using System;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class RiversForm : Form
    {
        private int selectedId = -1;


        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        public RiversForm()
        {
            InitializeComponent();
        }

        private void RiversForm_Load(object sender, EventArgs e)
        {
            dgvRivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRivers.MultiSelect = false;
            dgvRivers.ReadOnly = true;
            dgvRivers.AllowUserToAddRows = false;
            dgvRivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadRivers();
        }

        private void LoadRivers()
        {
            dgvRivers.DataSource = RiverService.GetAll();
        }

        private void dgvRivers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {
            
        }

        private void ClearInputs()
        {
            selectedId = -1;
            txtName.Clear();
            txtLength.Clear();
        }

        private double? ParseDouble(string s)
        {
            s = (s ?? "").Trim();
            if (s.Length == 0) return null;
            return double.TryParse(s, out var v) ? v : (double?)null;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Daryo nomini kiriting!");
                return;
            }

            RiverService.Add(txtName.Text.Trim(), ParseDouble(txtLength.Text));
            LoadRivers();
            ClearInputs();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan daryoni tanlang!");
                return;
            }

            RiverService.Update(selectedId, txtName.Text.Trim(), ParseDouble(txtLength.Text));
            LoadRivers();
            ClearInputs();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan daryoni tanlang!");
                return;
            }

            var res = MessageBox.Show("Tanlangan daryo o‘chirilsinmi?", "Tasdiqlash",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            RiverService.Delete(selectedId);
            LoadRivers();
            ClearInputs();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadRivers();
        }

        private void btnCountries_Click_1(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan daryoni tanlang!");
                return;
            }

            new RiverCountriesForm(selectedId).ShowDialog();
        }

        private void dgvRivers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvRivers.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["RiverID"].Value);

            txtName.Text = row.Cells["Name"].Value?.ToString();
            txtLength.Text = row.Cells["LengthKm"].Value?.ToString();
        }
    }
}
