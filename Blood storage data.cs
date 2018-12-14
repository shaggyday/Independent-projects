using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using blood_bankblood_bank_app;

namespace blood_bank_app
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] bloodStorage = database.bloodStorage();
            string bloodtypes = "A: " + bloodStorage[0] + Environment.NewLine +
                                "B: " + bloodStorage[1] + Environment.NewLine +
                                "O: " + bloodStorage[2] + Environment.NewLine +
                                "AB: " + bloodStorage[3] + Environment.NewLine;
            MessageBox.Show(bloodtypes, "Here it is!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.writeToCSVFile();
            MessageBox.Show("Doner data saved to CSV file", "Hooray!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form2();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}
