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

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            main_menu menu = new main_menu();
            menu.ShowDialog();
            Close();
        }

        private void Light_Click(object sender, EventArgs e)
        {
            Hide();
            middle game = new middle();
            game.ShowDialog();
            Close();
        }

        private void Middle_Click(object sender, EventArgs e)
        {
            Hide();
            middle game = new middle();
            game.ShowDialog();
            Close();
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            Hide();
            middle game = new middle();
            game.ShowDialog();
            Close();
        }
    }
}
