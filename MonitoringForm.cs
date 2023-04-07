using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncServerBotTelegram
{
    public partial class MonitoringForm : Form
    {
        public MonitoringForm()
        {
            InitializeComponent();

        }

        private void MonitoringForm_Load(object sender, EventArgs e)
        {
            
        }

        public void AddUser(string name,string id,string date)
        {
            listBox1.Invoke(new Action(() =>
            {
                string exc = $"{name}|{id}|{date}";
                bool InHave = false;
                foreach (var v in listBox1.Items)
                {
                    if (v.ToString().Split('|')[1] == id)
                        InHave = true;
                }
                if (!InHave)
                    listBox1.Items.Add(exc);
                else
                {
                    for(int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i].ToString().Split('|')[1] == id)
                        {
                            //change it user for new data
                            listBox1.Items.RemoveAt(i);
                            listBox1.Items.Insert(i, exc);
                            break;
                        }
                    }
                }
                
            }));
        }
    }
}
