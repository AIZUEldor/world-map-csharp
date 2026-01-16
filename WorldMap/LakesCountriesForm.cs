using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class LakesCountriesForm : Form
    {
        private readonly int _lakeId;

        private class CountryItem
        {
            public int CountryID { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        public LakesCountriesForm(int lakeId)
        {
            InitializeComponent();
            _lakeId = lakeId;
        }

        public LakesCountriesForm() : this(0) { }

        private void LakeCountriesForm_Load(object sender, EventArgs e)
        {
            if (_lakeId <= 0)
            {
                lblLake.Text = "Lake: tanlanmagan";
                return;
            }

            lblLake.Text = "Lake: " + LakeService.GetNameById(_lakeId);

            var countries = CountryService.GetAllForCombo()
                .Select(c => new CountryItem { CountryID = c.CountryID, Name = c.Name })
                .ToList();

            clbCountries.Items.Clear();
            foreach (var c in countries)
                clbCountries.Items.Add(c);

            var selectedIds = LakeCountriesService.GetCountryIdsByLake(_lakeId);

            for (int i = 0; i < clbCountries.Items.Count; i++)
            {
                var item = (CountryItem)clbCountries.Items[i];
                if (selectedIds.Contains(item.CountryID))
                    clbCountries.SetItemChecked(i, true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_lakeId <= 0)
            {
                MessageBox.Show("Ko‘l tanlanmagan!");
                return;
            }

            var ids = new List<int>();
            foreach (var item in clbCountries.CheckedItems)
                ids.Add(((CountryItem)item).CountryID);

            LakeCountriesService.Save(_lakeId, ids);

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

        private void LakesCountriesForm_Load(object sender, EventArgs e)
        {

        }

        private void clbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
