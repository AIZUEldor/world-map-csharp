using System;
using System.Windows.Forms;
using WorldMap.Services;
using WorldMap.Models;
using System.Linq.Expressions;

namespace WorldMap
{
    public partial class CountriesForm : Form
    {
        private int selectedId = -1;

        public CountriesForm()
        {
            InitializeComponent();
        }

        private void CountriesForm_Load(object sender, EventArgs e)
        {

            LoadContinents();
            LoadCountries();

            // UI sifat
            dgvCountries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCountries.MultiSelect = false;
            dgvCountries.ReadOnly = true;
            dgvCountries.AllowUserToAddRows = false;
            dgvCountries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadContinents()
        {
            var continents = ContinentService.GetAll();
            cmbContinent.DataSource = continents;
            cmbContinent.DisplayMember = "Name";
            cmbContinent.ValueMember = "ContinentID";
        }

        private void LoadCountries()
        {
            dgvCountries.DataSource = CountryService.GetAll();
        }
        private void ClearInputs()
        {
            selectedId = -1;
            txtName.Clear();
            txtISO.Clear();
            txtCurrency.Clear();
            txtPopulation.Clear();
            txtArea.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Davlat nomini kiriting!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtISO.Text) || txtISO.Text.Trim().Length != 3)
            {
                MessageBox.Show("ISOCode 3 ta harfdan iborat bo‘lishi kerak (masalan: UZB)!");
                return false;
            }

            return true;
        }

        private string NullIfEmpty(string s)
        {
            s = (s ?? "").Trim();
            return s.Length == 0 ? null : s;
        }

        private long? ParseLong(string s)
        {
            s = (s ?? "").Trim();
            if (s.Length == 0) return null;
            if (long.TryParse(s, out var v)) return v;

            MessageBox.Show("Population son bo‘lishi kerak!");
            return null;
        }

        private double? ParseDouble(string s)
        {
            s = (s ?? "").Trim();
            if (s.Length == 0) return null;
            if (double.TryParse(s, out var v)) return v;

            MessageBox.Show("AreaKm2 son bo‘lishi kerak!");
            return null;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            CountryService.Add(
                txtName.Text.Trim(),
                txtISO.Text.Trim().ToUpper(),
                (int)cmbContinent.SelectedValue,
                NullIfEmpty(txtCurrency.Text),
                ParseLong(txtPopulation.Text),
                ParseDouble(txtArea.Text)
            );

            LoadCountries();
            ClearInputs();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan davlatni tanlang!");
                return;
            }

            if (!ValidateInputs()) return;

            CountryService.Update(
                selectedId,
                txtName.Text.Trim(),
                txtISO.Text.Trim().ToUpper(),
                (int)cmbContinent.SelectedValue,
                NullIfEmpty(txtCurrency.Text),
                ParseLong(txtPopulation.Text),
                ParseDouble(txtArea.Text)
            );

            LoadCountries();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan davlatni tanlang!");
                return;
            }

            var res = MessageBox.Show(
                "Tanlangan davlat o‘chirilsinmi?",
                "Tasdiqlash",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (res != DialogResult.Yes) return;

            CountryService.Delete(selectedId);
            LoadCountries();
            ClearInputs();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadCountries();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtISO_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtCurrency_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvCountries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("RowIndex = " + e.RowIndex);
            if (e.RowIndex < 0) return;

            var row = dgvCountries.Rows[e.RowIndex];

            // ID ni doim nomi bilan oling
            selectedId = Convert.ToInt32(row.Cells["CountryID"].Value);

            txtName.Text = row.Cells["Name"].Value?.ToString() ?? "";
            txtISO.Text = row.Cells["ISOCode"].Value?.ToString() ?? "";
            txtCurrency.Text = row.Cells["Currency"].Value?.ToString() ?? "";
            txtPopulation.Text = row.Cells["Population"].Value?.ToString() ?? "";
            txtArea.Text = row.Cells["AreaKm2"].Value?.ToString() ?? "";

            // Continent tanlash
            string contName = row.Cells["Continent"].Value?.ToString() ?? "";

            for (int i = 0; i < cmbContinent.Items.Count; i++)
            {
                if (((Continent)cmbContinent.Items[i]).Name == contName)
                {
                    cmbContinent.SelectedIndex = i;
                    break;
                }
            }
        }

        private void cmbContinent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
