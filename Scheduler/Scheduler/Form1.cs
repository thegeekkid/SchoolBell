using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASquare.WindowsTaskScheduler.Interface;


namespace Scheduler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Text = DateTime.Now.Hour.ToString();
            textBox2.Text = DateTime.Now.Minute.ToString();
            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scheduleIt();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playsound("0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            playsound("1");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            playsound("2");
        }

        private void playsound(string num)
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\bell" + num + ".wav");
                player.PlaySync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void scheduleIt()
        {
            
        }
    }
}
