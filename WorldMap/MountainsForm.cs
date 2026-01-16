using System;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class MountainsForm : Form
    {
        private int selectedId = -1;
        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHeight_TextChanged(object sender, EventArgs e) { }

        private void dgvMountains_CellContentClick(object sender , EventArgs e)
        {

        }

        public MountainsForm()
        {
            InitializeComponent();
        }

        private void MountainsForm_Load(object sender, EventArgs e)
        {
            dgvMountains.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMountains.MultiSelect = false;
            dgvMountains.ReadOnly = true;
            dgvMountains.AllowUserToAddRows = false;
            dgvMountains.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadMountains();
        }

        private void LoadMountains()
        {
            dgvMountains.DataSource = MountainService.GetAll();
        }

        private void dgvMountains_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvMountains.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["MountainID"].Value);

            txtName.Text = row.Cells["Name"].Value?.ToString();
            txtHeight.Text = row.Cells["Height"].Value?.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tog‘ nomini kiriting!");
                return;
            }

            MountainService.Add(txtName.Text.Trim(), ParseInt(txtHeight.Text));
            LoadMountains();
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan tog‘ni tanlang!");
                return;
            }

            MountainService.Update(selectedId, txtName.Text.Trim(), ParseInt(txtHeight.Text));
            LoadMountains();
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan tog‘ni tanlang!");
                return;
            }

            if (MessageBox.Show("O‘chirilsinmi?", "Tasdiqlash",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            MountainService.Delete(selectedId);
            LoadMountains();
            Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMountains();
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan tog‘ni tanlang!");
                return;
            }

            new MountainsCountriesForm(selectedId).ShowDialog();
        }

        private void Clear()
        {
            selectedId = -1;
            txtName.Clear();
            txtHeight.Clear();
        }

        private int? ParseInt(string s)
        {
            s = (s ?? "").Trim();
            if (s.Length == 0) return null;
            return int.TryParse(s, out var v) ? v : (int?)null;
        }
    }
}
