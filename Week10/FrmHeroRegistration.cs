using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week10
{
    public partial class FrmHeroRegistration : Form
    {
        List<Hero> heroList;
        public FrmHeroRegistration()
        {
            InitializeComponent();
            InitGui();
        }
        void InitGui()
        {
            //initialise heroList
            //display all the heros
            heroList=Hero.CreateHeros();
            dgvHeros.DataSource = heroList;

            //populate the combobox with the hero's powers
            cboPower.DataSource = Enum.GetNames(typeof(PowerEnum));

            //set the initial age
            updownAge.Value = 30;

            //set is good
            chkIsGood.Checked = true;

            //initialise the name
            txtName.Text = "Rusian";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //use the form info to create a hero
            string name = txtName.Text;
            int age = (int)updownAge.Value;
            bool isGood = chkIsGood.Checked;
            PowerEnum power = (PowerEnum) Enum.Parse(typeof(PowerEnum), cboPower.SelectedItem.ToString());
            Hero hero = new Hero(name, age, isGood, power);

            //add hero to collection
            heroList.Add(hero);

            //refresh the data grid view
            dgvHeros.DataSource = null;
            dgvHeros.DataSource= heroList;
        }
    }
}
