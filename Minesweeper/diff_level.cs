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
    public partial class diff_level : Form
    {
        public diff_level()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            main_menu menu = new main_menu();
            menu.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            easy game = new easy();
            Close();
            game.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            middle game = new middle();
            Close();
            game.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            hard game = new hard();
            Close();
            game.ShowDialog();
        }
    }
}
