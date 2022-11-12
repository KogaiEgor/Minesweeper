using Minesweeper.Controllers;

namespace Minesweeper
{
    public partial class middle : Form
    {
        public middle()
        {
            InitializeComponent();
            MapController middle = new MapController(8, 10, 12);
            MapController.Init(this);
        }
    }
}