using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Minesweeper;

namespace Minesweeper.Controllers
{
    public class Gameplay
    {
        public static int mapSize;

        public static int numsofbomb_low;

        public static int numsofbomb_high;

        public static int numsofbomb;

        public static int time_sum;

        public static string time_str;

        public static string difflvl;

        static int m = 0, s = 0;

        static Label label = new Label();

        static System.Windows.Forms.Timer timer;

        public const int cellSize = 50;

        public static int currentPictureToSet = 0;

        public static int[,] map = new int[mapSize, mapSize];

        public static Button[,] buttons = new Button[mapSize, mapSize];

        public static Image spriteSet;

        public static bool isFirstStep;

        public static Point firstCoord;

        public static Form form;
        public Gameplay()
        {

        }

        public Gameplay(int map_size, int bomb_low, int bomb_high, string _difflvl)
        {
            mapSize = map_size;
            numsofbomb_low = bomb_low;
            numsofbomb_high = bomb_high;
            difflvl = _difflvl;
            map = new int[mapSize, mapSize];
            buttons = new Button[mapSize, mapSize];
        }

        public static void ConfigureMapSize(Form current)
        {
            current.Width = mapSize * cellSize + 220;
            current.Height = (mapSize + 1) * cellSize;
        }

        public static void InitMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public static void Init(Form current)
        {
            form = current;
            currentPictureToSet = 0;
            isFirstStep = true;
            spriteSet = new Bitmap("..\\..\\..\\Sprites\\buttons.png");
            ConfigureMapSize(current);
            InitMap();
            InitButtons(current);
            InitTimer(current);
        }

        public static void InitButtons(Form current)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Image = FindNeededImage(0, 0);
                    button.MouseUp += new MouseEventHandler(OnButtonPressedMouse);
                    current.Controls.Add(button);
                    buttons[i, j] = button;
                }
            }
        }

        public static void InitTimer(Form current)
        {
            timer = new System.Windows.Forms.Timer();
            label.Location = new Point(mapSize * cellSize + 40, mapSize * cellSize / 2 - 10);
            label.Size = new Size(cellSize + 37, 40);
            label.Text = string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            label.Font = new Font("Calibri", 22);

            timer.Tick += new EventHandler(Start_timer);
            timer.Interval = 1000;   
            timer.Enabled = true;

            current.Controls.Add(label);
        }
            
        private static void Start_timer(object sender, EventArgs e)
        {
            s++;
            if (s >= 60)
            {
                m++;
                s = 0;
            }
            label.Text = string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }

        public static void OnButtonPressedMouse(object sender, MouseEventArgs e)
        {
            Button pressedButton = sender as Button;
            switch (e.Button.ToString())
            {
                case "Right":
                    OnRightButtonPressed(pressedButton);
                    break;
                case "Left":
                    OnLeftButtonPressed(pressedButton);
                    break;
            }
        }

        public static void OnRightButtonPressed(Button pressedButton)
        {
            currentPictureToSet++;
            currentPictureToSet %= 3;
            int posX = 0;
            int posY = 0;
            switch (currentPictureToSet)
            {
                case 0:
                    posX = 0;
                    posY = 0;
                    break;
                case 1:
                    posX = 0;
                    posY = 2;
                    break;
                case 2:
                    posX = 2;
                    posY = 2;
                    break;
            }
            pressedButton.Image = FindNeededImage(posX, posY);
        }

        public static void OnLeftButtonPressed(Button pressedButton)
        {
            pressedButton.Enabled = false;
            int iButton = pressedButton.Location.Y / cellSize;
            int jButton = pressedButton.Location.X / cellSize;
            if (isFirstStep)
            {
                timer.Start();
                firstCoord = new Point(jButton, iButton);
                SeedMap();
                CountCellBomb();
                isFirstStep = false;
            }
            OpenCells(iButton, jButton);

            if (map[iButton, jButton] == -1)
            {
                timer.Stop();
                ShowAllBombs(iButton, jButton);
                MessageBox.Show("Поражение!");
                form.Hide();
                form.Controls.Clear();
                result result = new result();
                result.label1.Text = "Ваше время: " + string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
                result.label2.Text = "Поражение";
                result.difflvl = difflvl;
                result.Init();
                m = 0;
                s = 0;
                form.Close();
                result.ShowDialog();
            }
            else if (IsEnd() == true)
            {
                Victory(iButton, jButton);
            }
        }

        private static void Victory(int iButton, int jButton)
        {
            timer.Stop();
            ShowAllBombs(iButton, jButton);
            time_sum = m * 60 + s;
            MessageBox.Show("Победа!");
            form.Hide();
            form.Controls.Clear();
            result result = new result();
            result.label1.Text = "Ваше время: " + string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            time_str = string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            result.label2.Text = "Победа";
            result.difflvl = difflvl;
            result.time = time_sum;
            result.Init();
            m = 0;
            s = 0;
            form.Close();
            result.ShowDialog();
        }

        public static bool IsEnd()
        {
            int countbombs = 0, countbuttons = 0;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (buttons[i, j].Enabled)
                    {
                        countbuttons++;
                        if (map[i, j] == -1)
                        {
                            countbombs++;
                            if (countbuttons == countbombs && countbombs == numsofbomb)
                            {
                                return true;
                            } else if (i == mapSize && j == mapSize)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    } else
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        public static void ShowAllBombs(int iBomb, int jBomb)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (i == iBomb && j == jBomb)
                        continue;
                    if (map[i, j] == -1)
                    {
                        buttons[i, j].Image = FindNeededImage(3, 2);
                    }
                }
            }
        }

        public static Image FindNeededImage(int xPos, int yPos)
        {
            Image image = new Bitmap(cellSize, cellSize);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(spriteSet, new Rectangle(new Point(0, 0), new Size(cellSize, cellSize)), 0 + 32 * xPos, 0 + 32 * yPos, 33, 33, GraphicsUnit.Pixel);


            return image;
        }

        public static void SeedMap()
        {
            Random r = new Random();
            int number = r.Next(numsofbomb_low, numsofbomb_high);
            numsofbomb = number;

            for (int i = 0; i < number; i++)
            {
                int posI = r.Next(0, mapSize - 1);
                int posJ = r.Next(0, mapSize - 1);

                while (map[posI, posJ] == -1 || (Math.Abs(posI - firstCoord.Y) <= 1 && Math.Abs(posJ - firstCoord.X) <= 1))
                {
                    posI = r.Next(0, mapSize - 1);
                    posJ = r.Next(0, mapSize - 1);
                }
                map[posI, posJ] = -1;
            }
        }

        public static void CountCellBomb()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == -1)
                    {
                        for (int k = i - 1; k < i + 2; k++)
                        {
                            for (int l = j - 1; l < j + 2; l++)
                            {
                                if (!IsInBorder(k, l) || map[k, l] == -1)
                                    continue;
                                map[k, l] = map[k, l] + 1;
                            }
                        }
                    }
                }
            }
        }

        public static void OpenCell(int i, int j)
        {
            buttons[i, j].Enabled = false;

            switch (map[i, j])
            {
                case 1:
                    buttons[i, j].Image = FindNeededImage(1, 0);
                    break;
                case 2:
                    buttons[i, j].Image = FindNeededImage(2, 0);
                    break;
                case 3:
                    buttons[i, j].Image = FindNeededImage(3, 0);
                    break;
                case 4:
                    buttons[i, j].Image = FindNeededImage(4, 0);
                    break;
                case 5:
                    buttons[i, j].Image = FindNeededImage(0, 1);
                    break;
                case 6:
                    buttons[i, j].Image = FindNeededImage(1, 1);
                    break;
                case 7:
                    buttons[i, j].Image = FindNeededImage(2, 1);
                    break;
                case 8:
                    buttons[i, j].Image = FindNeededImage(3, 1);
                    break;
                case -1:
                    buttons[i, j].Image = FindNeededImage(1, 2);
                    break;
                case 0:
                    buttons[i, j].Image = FindNeededImage(0, 0);
                    break;
            }
        }

        public static void OpenCells(int i, int j)
        {
            OpenCell(i, j);

            if (map[i, j] > 0)
                return;

            for (int k = i - 1; k < i + 2; k++)
            {
                for (int l = j - 1; l < j + 2; l++)
                {
                    if (!IsInBorder(k, l))
                        continue;
                    if (!buttons[k, l].Enabled)
                        continue;
                    if (map[k, l] == 0)
                        OpenCells(k, l);
                    else if (map[k, l] > 0)
                        OpenCell(k, l);
                }
            }
        }

        public static bool IsInBorder(int i, int j)
        {
            if (i < 0 || j < 0 || j > mapSize - 1 || i > mapSize - 1)
            {
                return false;
            }
            return true;
        }

        
    }
}