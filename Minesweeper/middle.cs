using Minesweeper.Controllers;

namespace Minesweeper
{
    public partial class middle : Form
    {
        public middle()
        {
            InitializeComponent();
            Game middle = new Game(8, 10, 12);
            Game.Init(this);
        }
    }
}