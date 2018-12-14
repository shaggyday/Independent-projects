using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blood_bank_app
{
    public partial class Form1 : Form
    {
        public String firstName;
        public String lastName;
        public String bloodType;
        public String RhFactor;
        public String address;
        public String phone;
        private TextBox[] TextBoxes;
        private ComboBox[] ComboBoxes;
        private String[] TextBoxData;
        private String[] ComboBoxData;


        public Form1()
        {
            InitializeComponent();
            TextBoxes = new TextBox[4] { textBox1,textBox2,textBox3,textBox4};
            ComboBoxes = new ComboBox[2] {comboBox1,comboBox2};
            TextBoxData = new string[4] {firstName,lastName,address,phone};
            ComboBoxData = new string[2] {bloodType,RhFactor};
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private Boolean validateTextBox(TextBox textBox)
        {
            return (textBox.Text != null);
        }

        private Boolean validateComboBox(ComboBox comboBox)
        {
            return (comboBox.SelectedIndex != -1);
        }

        private void errorMessage()
        {
            MessageBox.Show("Please fill out all fields", "Unfilled fields", MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void successMessage()
        {
            MessageBox.Show("Doner Data registered successfully", "Hooray!",  MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Data validation
            foreach (var t in TextBoxes)
            {
                if (!validateTextBox(t))
                {
                    errorMessage();
                    return;
                }
            }
            foreach (var c in ComboBoxes)
            {
                if (!validateComboBox(c))
                {
                    errorMessage();
                    return;
                }
            }

            //Pass data
            for (int i = 0; i < TextBoxes.Length; i++)
                TextBoxData[i] = TextBoxes[i].Text;
            for (int i = 0; i < ComboBoxes.Length; i++)
                ComboBoxData[i] = ComboBoxes[i].SelectedItem.ToString();
            
            UserInterface Doner = new UserInterface(TextBoxData[0],TextBoxData[1],
                ComboBoxData[0],ComboBoxData[1],TextBoxData[2],TextBoxData[3]);

            //flush
            successMessage();
            clearFields();
        }

        private void clearFields()
        {
            foreach(var t in TextBoxes)
            {
                t.Clear();
            }

            foreach (var c in ComboBoxes)
            {
                c.SelectedIndex = -1;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
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
    }
}
