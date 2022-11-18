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
    public partial class easy : Form
    {
        public easy()
        {
            InitializeComponent();
            Gameplay light = new Gameplay(6, 4, 6, "Easy");
            Gameplay.Init(this);
        }
    }
}
