using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class SeasCountriesForm : Form
    {
        private readonly int _seaId;

        private class CountryItem
        {
            public int CountryID { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        public SeasCountriesForm(int seaId)
        {
            InitializeComponent();
            _seaId = seaId;
        }

        // Designer xato bermasin deb (parametrsiz constructor)
        public SeasCountriesForm() : this(0) { }

        private void SeasCountriesForm_Load(object sender, EventArgs e)
        {
            if (_seaId <= 0)
            {
                lblSea.Text = "Sea: tanlanmagan";
                return;
            }

            lblSea.Text = "Sea: " + SeaService.GetNameById(_seaId);

            // country list
            var countries = CountryService.GetAllForCombo()
                .Select(c => new CountryItem { CountryID = c.CountryID, Name = c.Name })
                .ToList();

            clbCountries.Items.Clear();
            foreach (var c in countries)
                clbCountries.Items.Add(c);

            // already selected
            var selectedIds = SeaCountriesService.GetCountryIdsBySea(_seaId);

            for (int i = 0; i < clbCountries.Items.Count; i++)
            {
                var item = (CountryItem)clbCountries.Items[i];
                if (selectedIds.Contains(item.CountryID))
                    clbCountries.SetItemChecked(i, true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_seaId <= 0)
            {
                MessageBox.Show("Dengiz tanlanmagan!");
                return;
            }

            var ids = new List<int>();
            foreach (var item in clbCountries.CheckedItems)
                ids.Add(((CountryItem)item).CountryID);

            SeaCountriesService.Save(_seaId, ids);

            MessageBox.Show("Saqlandi ✅");
            Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCountries.Items.Count; i++)
                clbCountries.SetItemChecked(i, true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCountries.Items.Count; i++)
                clbCountries.SetItemChecked(i, false);
        }
    }
}
