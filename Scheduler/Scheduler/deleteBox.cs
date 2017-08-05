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
using Microsoft.Win32.TaskScheduler;

namespace Scheduler
{
    public partial class deleteBox : Form
    {
        public deleteBox()
        {
            InitializeComponent();
        }

        private void deleteBox_Load(object sender, EventArgs e)
        {
            loadTasks();
        }

        private void loadTasks()
        {
            listBox1.Items.Clear();
            if (Directory.Exists(Environment.CurrentDirectory + @"\ScheduleData\"))
            {
                DirectoryInfo dinfo;
                foreach (string task in Directory.GetDirectories(Environment.CurrentDirectory + @"\ScheduleData"))
                {
                    dinfo = new DirectoryInfo(task);
                    string item = "";
                    item = dinfo.Name + @" | ";
                    item += File.ReadAllText(task + @"\time.txt") + @" ";
                    item += File.ReadAllText(task + @"\frequency.txt") + @" - ";
                    item += File.ReadAllText(task + @"\zone.txt");
                    listBox1.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No more scheduled rings were found.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = listBox1.SelectedItem.ToString().Split('|')[0].TrimEnd();

            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(name);
            }

            Directory.Delete(Environment.CurrentDirectory + @"\ScheduleData\" + name, true);

            MessageBox.Show("Ring deleted.");
            loadTasks();
        }
    }
}
