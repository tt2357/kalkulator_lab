using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kalkulator
{
    public partial class Form2 : Form
    {
        string[] history;
        public Form2(string[] history)
        {
            this.history = history;
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Historia");
            foreach (string item in history)
            {
                ListViewItem listViewItem = new ListViewItem(item);
                listView1.Items.Add(listViewItem);
            }
        }
    }
}
