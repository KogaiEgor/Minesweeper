using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            diff_level diff_Level = new diff_level();
            diff_Level.ShowDialog();
            Close();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            RecordsForm records = new RecordsForm();
            records.ShowDialog();   
            Close();
        }
    }
}
