using System;
using System.Windows.Forms;
using System.Xml.Linq;
using WorldMap.Services;
using static System.Windows.Forms.MonthCalendar;

namespace WorldMap
{
    public partial class SeasForm : Form
    {
        private int selectedId = -1;

        public SeasForm()
        {
            InitializeComponent();
        }

        private void SeasForm_Load(object sender, EventArgs e)
        {
            dgvSeas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSeas.MultiSelect = false;
            dgvSeas.ReadOnly = true;
            dgvSeas.AllowUserToAddRows = false;
            dgvSeas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadSeas();
        }

        private void LoadSeas()
        {
            dgvSeas.DataSource = SeaService.GetAll();
        }

        private void dgvSeas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvSeas.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["SeaID"].Value);

            txtName.Text = row.Cells["Name"].Value?.ToString();
            txtArea.Text = row.Cells["AreaKm2"].Value?.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Dengiz nomini kiriting!");
                return;
            }

            SeaService.Add(txtName.Text.Trim(), ParseDouble(txtArea.Text));
            LoadSeas();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan dengizni tanlang!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Dengiz nomini kiriting!");
                return;
            }

            SeaService.Update(selectedId, txtName.Text.Trim(), ParseDouble(txtArea.Text));
            LoadSeas();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan dengizni tanlang!");
                return;
            }

            var res = MessageBox.Show(
                "Tanlangan dengiz o‘chirilsinmi?",
                "Tasdiqlash",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (res != DialogResult.Yes) return;

            SeaService.Delete(selectedId);
            LoadSeas();
            ClearInputs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSeas();
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Avval jadvaldan dengizni tanlang!");
                return;
            }

            // Many-to-Many: SeasCountries
            new SeasCountriesForm(selectedId).ShowDialog();
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

            // Agar vergul bilan yozsa ham ishlasin
            s = s.Replace(',', '.');

            return double.TryParse(s, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var v)
                ? v
                : (double?)null;
        }
    }
}
