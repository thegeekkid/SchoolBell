using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scheduler
{
    public partial class setup : Form
    {
        public setup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            File.Copy(textBox1.Text, Environment.CurrentDirectory + @"\bell0.wav");
            File.Copy(textBox2.Text, Environment.CurrentDirectory + @"\bell1.wav");
            File.Copy(textBox3.Text, Environment.CurrentDirectory + @"\bell2.wav");

            MessageBox.Show("Initial setup complete.  This form will now close, please re-launch the program to begin use.");
            Environment.Exit(0);
            Application.Exit();
            Close();
        }
    }
}
