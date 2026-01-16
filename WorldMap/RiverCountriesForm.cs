using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WorldMap.Services;

namespace WorldMap
{
    public partial class RiverCountriesForm : Form
    {
        private readonly int _riverId;

        // CheckedListBox uchun item
        private class CountryItem
        {
            public int CountryID { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        // RiversForm shu constructor bilan ochadi
        public RiverCountriesForm(int riverId)
        {
            InitializeComponent();
            _riverId = riverId;
        }

        // (ixtiyoriy) dizaynerga qulay bo‘lsin
        public RiverCountriesForm() : this(0) { }

        private void RiverCountriesForm_Load(object sender, EventArgs e)
        {
            if (_riverId <= 0)
            {
                lblRiver.Text = "River: (tanlanmagan)";
                return;
            }

            // Daryo nomi
            var riverName = RiverService.GetNameById(_riverId);
            lblRiver.Text = $"River: {riverName}";

            // Country listni yuklash
            var countries = CountryService.GetAllForCombo()
                .Select(c => new CountryItem
                {
                    CountryID = c.CountryID,
                    Name = c.Name
                })
                .ToList();

            clbCountries.Items.Clear();
            foreach (var c in countries)
                clbCountries.Items.Add(c);

            // Oldin bog‘langanlarini belgilash
            var selectedIds = RiverCountriesService.GetCountryIdsByRiver(_riverId);

            for (int i = 0; i < clbCountries.Items.Count; i++)
            {
                var item = (CountryItem)clbCountries.Items[i];
                clbCountries.SetItemChecked(i, selectedIds.Contains(item.CountryID));
            }

            // qulaylik
            clbCountries.CheckOnClick = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_riverId <= 0)
            {
                MessageBox.Show("Daryo tanlanmagan!");
                return;
            }

            var ids = new List<int>();
            foreach (var item in clbCountries.CheckedItems)
            {
                ids.Add(((CountryItem)item).CountryID);
            }

            RiverCountriesService.Save(_riverId, ids);

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
