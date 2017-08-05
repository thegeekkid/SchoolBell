using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.IO;

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
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            comboBox1.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                playsound("0");
            }else
            {
                if (e.KeyCode == Keys.F2)
                {
                    playsound("1");
                }
                else
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        playsound("2");
                    }
                }
            }
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

            if (Directory.Exists(Environment.CurrentDirectory + @"\ScheduleData\" + textBox3.Text)) {
                MessageBox.Show("Error: A schedule with the same name already exists.  Please enter another name.");
            }else
            {
                string path = Environment.CurrentDirectory + @"\ScheduleData\" + textBox3.Text + @"\";
                Directory.CreateDirectory(path);

                File.WriteAllText(path + "frequency.txt", comboBox2.SelectedItem.ToString());
                File.WriteAllText(path + "zone.txt", comboBox1.SelectedItem.ToString());
                File.WriteAllText(path + "date.txt", dateTimePicker1.Value.ToShortDateString());
                File.WriteAllText(path + "time.txt", textBox1.Text + @":" + textBox2.Text);

                DateTime dt = new DateTime();
                dt = DateTime.Parse(dateTimePicker1.Value.ToShortDateString() + " " + textBox1.Text + ":" + textBox2.Text);



                using (TaskService ts = new TaskService())
                {
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = textBox3.Text;

                    if (comboBox2.SelectedIndex == 0)
                    {
                        DailyTrigger dTrigger = new DailyTrigger();
                        dTrigger.StartBoundary = dt;
                        dTrigger.DaysInterval = 1;
                        td.Triggers.Add(dTrigger);
                    }
                    else
                    {
                        if (comboBox2.SelectedIndex == 1)
                        {
                            WeeklyTrigger wTrigger = new WeeklyTrigger();
                            wTrigger.StartBoundary = dt;
                            if (dt.DayOfWeek == DayOfWeek.Monday)
                            {
                                wTrigger.DaysOfWeek = DaysOfTheWeek.Monday;
                            }
                            else
                            {
                                if (dt.DayOfWeek == DayOfWeek.Tuesday)
                                {
                                    wTrigger.DaysOfWeek = DaysOfTheWeek.Tuesday;
                                }
                                else
                                {
                                    if (dt.DayOfWeek == DayOfWeek.Wednesday)
                                    {
                                        wTrigger.DaysOfWeek = DaysOfTheWeek.Wednesday;
                                    }
                                    else
                                    {
                                        if (dt.DayOfWeek == DayOfWeek.Thursday)
                                        {
                                            wTrigger.DaysOfWeek = DaysOfTheWeek.Thursday;
                                        }
                                        else
                                        {
                                            if (dt.DayOfWeek == DayOfWeek.Friday)
                                            {
                                                wTrigger.DaysOfWeek = DaysOfTheWeek.Friday;
                                            }
                                            else
                                            {
                                                if (dt.DayOfWeek == DayOfWeek.Saturday)
                                                {
                                                    wTrigger.DaysOfWeek = DaysOfTheWeek.Saturday;
                                                }
                                                else
                                                {
                                                    wTrigger.DaysOfWeek = DaysOfTheWeek.Sunday;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            wTrigger.WeeksInterval = 1;
                            td.Triggers.Add(wTrigger);
                        }
                        else
                        {
                            TimeTrigger tt = new TimeTrigger();
                            tt.StartBoundary = dt;
                            td.Triggers.Add(tt);
                        }

                    }

                    td.Actions.Add(new ExecAction(Environment.CurrentDirectory + @"\SchoolBell-ring.exe", comboBox1.SelectedIndex.ToString(), Environment.CurrentDirectory));

                    ts.RootFolder.RegisterTaskDefinition(textBox3.Text, td);
                }

                MessageBox.Show("Bell scheduled.");
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            deleteBox db = new deleteBox();
            db.ShowDialog();
        }
    }
}
