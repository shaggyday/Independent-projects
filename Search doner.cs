using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using blood_bankblood_bank_app;

namespace blood_bank_app
{
    public partial class Form3 : Form
    {
        private string[] DonerData;
        public Form3()
        {
            InitializeComponent();
        }

        private void clearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form2();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DonerData = database.searchDataBase(textBox1.Text, textBox2.Text);
            if (DonerData == null)
                MessageBox.Show("Doner information not found", "Doner not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(convertToString(DonerData), "Found it!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearFields();
            
        }

        private string convertToString(string[] DonerData)
        {
            return ("Name: " + DonerData[0] + " " + DonerData[1] + Environment.NewLine +
                    "Blood Type: " + DonerData[2] + Environment.NewLine +
                    "Rh Factor: " + DonerData[3] + Environment.NewLine +
                    "Address: " + DonerData[4] + Environment.NewLine +
                    "Phone: " + DonerData[5]);
        }
    }
}
