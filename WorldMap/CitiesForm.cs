using System;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class CitiesForm : Form
    {
        private int selectedId = -1;

        public CitiesForm()
        {
            InitializeComponent();
        }


        private void CitiesForm_Load(object sender, EventArgs e)
        {
            // Country combobox
            cmbCountry.DataSource = CountryService.GetAllForCombo();
            cmbCountry.DisplayMember = "Name";
            cmbCountry.ValueMember = "CountryID";
            cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;

            // Grid settings
            dgvCities.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCities.MultiSelect = false;
            dgvCities.ReadOnly = true;
            dgvCities.AllowUserToAddRows = false;
            dgvCities.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadCities();
        }

        private void LoadCities()
        {
            dgvCities.DataSource = CityService.GetAll();
        }

        private void dgvCities_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvCities.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["CityID"].Value);
            txtName.Text = row.Cells["Name"].Value?.ToString();
            txtPopulation.Text = row.Cells["Population"].Value?.ToString();

            cmbCountry.SelectedValue = Convert.ToInt32(row.Cells["CountryID"].Value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            CityService.Add(
                txtName.Text.Trim(),
                (int)cmbCountry.SelectedValue,
                ParseLong(txtPopulation.Text)
            );

            LoadCities();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan shaharni tanlang!");
                return;
            }

            if (!ValidateInputs()) return;

            CityService.Update(
                selectedId,
                txtName.Text.Trim(),
                (int)cmbCountry.SelectedValue,
                ParseLong(txtPopulation.Text)
            );

            LoadCities();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan shaharni tanlang!");
                return;
            }

            var res = MessageBox.Show(
                "Tanlangan shahar o‘chirilsinmi?",
                "Tasdiqlash",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            CityService.Delete(selectedId);
            LoadCities();
            ClearInputs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCities();
        }

        private void ClearInputs()
        {
            selectedId = -1;
            txtName.Clear();
            txtPopulation.Clear();
            if (cmbCountry.Items.Count > 0) cmbCountry.SelectedIndex = 0;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Shahar nomini kiriting!");
                return false;
            }
            return true;
        }

        private long? ParseLong(string s)
        {
            s = (s ?? "").Trim();
            if (s.Length == 0) return null;

            if (long.TryParse(s, out var v)) return v;

            MessageBox.Show("Aholi soni faqat son bo‘lishi kerak!");
            return null;
        }
    }
}
