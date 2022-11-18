using Minesweeper.Controllers;

namespace Minesweeper
{
    public partial class middle : Form
    {
        public middle()
        {
            InitializeComponent();
            Gameplay middle = new Gameplay(8, 10, 12, "Middle");
            Gameplay.Init(this);
        }
    }
}