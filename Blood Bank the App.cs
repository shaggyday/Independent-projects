﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using blood_bankblood_bank_app;

namespace blood_bank_app
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
//            if (!File.Exists(database.Text_path))
//                File.Create(database.Text_path);
//            database.readData();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form1();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form3();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form4();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form5();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}