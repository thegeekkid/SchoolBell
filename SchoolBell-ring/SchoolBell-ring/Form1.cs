using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolBell_ring
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string num = "0";

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                num = Environment.GetCommandLineArgs()[1];
            }
            catch
            {
                num = "0";
            }
            if (num == "")
            {
                num = "0";
            }
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Environment.CurrentDirectory + @"\bell" + num + ".wav");
            player.PlaySync();
            Environment.Exit(0);
            Application.Exit();
            Close();
        }
    }
}
