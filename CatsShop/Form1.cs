using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatsShop
{
    public partial class Form1 : Form
    {
        DBManager dBManager = new DBManager();
        public Form1()
        {
            InitializeComponent();
            numericUpDown2.Maximum = int.MaxValue;
            ListUpdate();
            listBox1.DoubleClick += ListBox1_DoubleClick;
            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dBManager.Dispose();
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int i = 0;
            foreach (var item in cats.Keys)
            {
                if (i == index)
                {
                    dBManager.Delete(item);
                    break;
                }
                i++;
            }
            ListUpdate();
        }

        Dictionary<int, string> cats;
        private void ListUpdate()
        {
            listBox1.Items.Clear();
            cats = dBManager.GetAllCats();
            foreach (var item in cats.Values)
            {
                listBox1.Items.Add(item);
            }
        }

        ColorDialog dialog = new ColorDialog();
        private void button2_Click(object sender, EventArgs e)
        {
            dialog.ShowDialog();
            button2.BackColor = dialog.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dBManager.Add((int)numericUpDown1.Value, numericUpDown2.Value, dialog.Color.ToString());
            ListUpdate();
            numericUpDown1.Value = numericUpDown2.Value = 0;
            button2.BackColor = Color.Empty;
        }

    }
}
