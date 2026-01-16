using System;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class LakesForm : Form
    {
        private int selectedId = -1;

        public LakesForm()
        {
            InitializeComponent();
        }

        private void LakesForm_Load(object sender, EventArgs e)
        {
            dgvLakes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLakes.MultiSelect = false;
            dgvLakes.ReadOnly = true;
            dgvLakes.AllowUserToAddRows = false;
            dgvLakes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadLakes();
        }

        private void LoadLakes()
        {
            dgvLakes.DataSource = LakeService.GetAll();
        }

        private void dgvLakes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvLakes.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["LakeID"].Value);

            txtName.Text = row.Cells["Name"].Value?.ToString();
            txtArea.Text = row.Cells["AreaKm2"].Value?.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Ko‘l nomini kiriting!");
                return;
            }

            LakeService.Add(txtName.Text.Trim(), ParseDouble(txtArea.Text));
            LoadLakes();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan ko‘lni tanlang!");
                return;
            }

            LakeService.Update(selectedId, txtName.Text.Trim(), ParseDouble(txtArea.Text));
            LoadLakes();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan ko‘lni tanlang!");
                return;
            }

            var res = MessageBox.Show("Tanlangan ko‘l o‘chirilsinmi?", "Tasdiqlash",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            LakeService.Delete(selectedId);
            LoadLakes();
            ClearInputs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLakes();
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan ko‘lni tanlang!");
                return;
            }

            new LakesCountriesForm(selectedId).ShowDialog();
        }

        private void ClearInputs()
        {
            selectedId = -1;
            txtName.Clear();
            txtArea.Clear();
        }

        private double? ParseDouble(string s)
        {
            s = (s ?? "").Trim();
            if (s.Length == 0) return null;
            return double.TryParse(s, out var v) ? v : (double?)null;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
