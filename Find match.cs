using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using blood_bankblood_bank_app;

namespace blood_bank_app
{
    public partial class Form4 : Form
    {
        private string bloodType;
        private string RhFactor;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bloodType = comboBox1.SelectedItem.ToString();
            RhFactor = comboBox2.SelectedItem.ToString();
            writeToCSVFile(database.findMatch(bloodType, RhFactor));
            MessageBox.Show("Matches saved to CSV file", "Hooray!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form2();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void writeToCSVFile(List<String[]> matches)  
        {
            using (var CSV_Data = new StreamWriter(database.CSV_path + "_" + bloodType + RhFactor + ".csv"))
            {
                var CSV_DonerData = new CsvWriter(CSV_Data);
                foreach (var a in database.CSV_headers)
                    CSV_DonerData.WriteField(a);
                CSV_DonerData.NextRecord();
                foreach (var d in matches)
                {
                    foreach (var e in d)
                    {
                        CSV_DonerData.WriteField(e);
                    }

                    CSV_DonerData.NextRecord();
                }
            }
        }
    }
}
