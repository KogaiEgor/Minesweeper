using Minesweeper.Controllers;

namespace Minesweeper
{
    public partial class middle : Form
    {
        public middle()
        {
            InitializeComponent();
            MapController.Init(this);
        }
    }
}