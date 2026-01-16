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
    public partial class MountainsCountriesForm : Form
    {
        private readonly int _mountainId;

        private class CountryItem
        {
            public int CountryID { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }
        public MountainsCountriesForm(int mountainId)
        {
            InitializeComponent();
            _mountainId = mountainId;
        }

        // Designer xato bermasin deb
        public MountainsCountriesForm() : this(0) { }

        private void lblMountain_Click(object sender, EventArgs e)
        {

        }

        private void clbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCountries.Items.Count; i++)
                clbCountries.SetItemChecked(i, false);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCountries.Items.Count; i++)
                clbCountries.SetItemChecked(i, true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mountainId <= 0)
            {
                MessageBox.Show("Tog‘ tanlanmagan!");
                return;
            }

            var ids = new List<int>();
            foreach (var item in clbCountries.CheckedItems)
                ids.Add(((CountryItem)item).CountryID);

            MountainCountriesService.Save(_mountainId, ids);

            MessageBox.Show("Saqlandi ✅");
            Close();
        }

        private void MountainsCountriesForm_Load(object sender, EventArgs e)
        {
            if (_mountainId <= 0)
            {
                lblMountain.Text = "Mountain: tanlanmagan";
                return;
            }

            lblMountain.Text = "Mountain: " + MountainService.GetNameById(_mountainId);

            // Countries list
            var countries = CountryService.GetAllForCombo()
                .Select(c => new CountryItem { CountryID = c.CountryID, Name = c.Name })
                .ToList();

            clbCountries.Items.Clear();
            foreach (var c in countries)
                clbCountries.Items.Add(c);

            // Already selected
            var selectedIds = MountainCountriesService.GetCountryIdsByMountain(_mountainId);

            for (int i = 0; i < clbCountries.Items.Count; i++)
            {
                var item = (CountryItem)clbCountries.Items[i];
                if (selectedIds.Contains(item.CountryID))
                    clbCountries.SetItemChecked(i, true);
            }

        }
    }
}
