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
    public partial class hard : Form
    {
        public hard()
        {
            InitializeComponent();
            Gameplay hard = new Gameplay(12, 14, 16, "Hard");
            Gameplay.Init(this);
        }
    }
}
