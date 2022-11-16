using Minesweeper.Controllers;
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
    public partial class light : Form
    {
        public light()
        {
            InitializeComponent();
            Game light = new Game(6, 6, 8);
            Game.Init(this);
        }
    }
}
