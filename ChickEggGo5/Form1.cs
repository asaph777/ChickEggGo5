using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChickEggGo5
{
    public partial class Form1 : Form
    {
        List<TableRequests> tablReqs = new List<TableRequests>();
        Server server = new Server();
        Cook cook1 = new Cook();
        Cook cook2 = new Cook();
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string countChickens = textBox1.Text;
                string countEggs = textBox2.Text;

                string drinks = comboBox1.SelectedItem.ToString();
                string nameCistomer = textBox3.Text;

                if (Server.counReqs >= 8)
                {
                    button1.Enabled = false;
                    MessageBox.Show("More than 8 orders in one table!");
                    return;
                }

                server.Recieve(Int32.Parse(countChickens), Int32.Parse(countEggs), drinks, nameCistomer);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        //TODO: not used "lock", LINQ, ThreadSleep
        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cook cook = null;
                while (cook == null)
                {
                    if (cook1.flag == 0)
                    {
                        cook = cook1;
                    }
                    else if (cook2.flag == 0)
                    {
                        cook = cook2;
                    }
                }
                try
                {
                    tablReqs.Add(server.GetTableRequests());
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);
                    return;
                }
                TableRequests table = tablReqs[tablReqs.Count - 1];
                Task<TableRequests> cookProcess = Task.Run(() => cook.Process(table));
                Task.WaitAll(cookProcess);
                Task<List<string>> serve = Task.Run(() => server.Serve());
                foreach (var item in serve.Result)
                {
                    listBox1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            button1.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
