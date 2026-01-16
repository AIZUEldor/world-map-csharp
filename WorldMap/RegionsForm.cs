using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class RegionsForm : Form
    {
        private int selectedId = -1;
        public RegionsForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvRegions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvRegions.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["RegionID"].Value);

            txtName.Text = row.Cells["Name"].Value?.ToString();
            cmbCountry.SelectedValue = Convert.ToInt32(row.Cells["CountryID"].Value);
        }
        private void Clear()
        {
            selectedId = -1;
            txtName.Clear();
            cmbCountry.SelectedIndex = 0;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRegions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan region tanlang!");
                return;
            }

            var res = MessageBox.Show("Region o‘chirilsinmi?", "Tasdiqlash",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            RegionService.Delete(selectedId);
            LoadRegions();
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan region tanlang!");
                return;
            }

            RegionService.Update(selectedId, txtName.Text.Trim(), (int)cmbCountry.SelectedValue);
            LoadRegions();
            Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Region nomini kiriting!");
                return;
            }

            RegionService.Add(txtName.Text.Trim(), (int)cmbCountry.SelectedValue);
            LoadRegions();
            Clear();
        }

        private void RegionsForm_Load(object sender, EventArgs e)
        {
            cmbCountry.DataSource = CountryService.GetAllForCombo();
            cmbCountry.DisplayMember = "Name";
            cmbCountry.ValueMember = "CountryID";

            dgvRegions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRegions.MultiSelect = false;
            dgvRegions.ReadOnly = true;
            dgvRegions.AllowUserToAddRows = false;
            dgvRegions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadRegions();
        }
        private void LoadRegions()
        {
            dgvRegions.DataSource = RegionService.GetAll();
        }
    }
}
