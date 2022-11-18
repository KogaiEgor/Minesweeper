using Minesweeper.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class RecordsForm : Form
    {
        private LinkedList<Records> rlist = new LinkedList<Records>();
        public RecordsForm()
        {
            GetRecords();
            InitializeComponent();
            InitButton(this);
            InitRecords(this, "Easy");
            InitMenu(this);
        }
        private void GetRecords()
        {
            string filename = "Record.bin";
            using BinaryReader binaryReader = new BinaryReader(File.Open(filename, FileMode.OpenOrCreate)); ;
            while (binaryReader.PeekChar() != -1)
            {
                Records records = new Records();
                records.Name = binaryReader.ReadString();
                records.Diff = binaryReader.ReadString();
                records.Time = binaryReader.ReadInt32();
                records.Time_String = binaryReader.ReadString();
                rlist.AddLast(records);
            }
            binaryReader.Close();
        }
        private void InitRecords(Form current, string difflvl)
        {
            Label main_label = new Label();
            main_label.Location = new Point(170, 20);
            main_label.Text = "Таблица рекордов";
            main_label.Size = new Size(400, 40);
            main_label.Font = new Font("Calibri", 24F);
            current .Controls.Add(main_label);
            int count = 1;
            int loc_y = 30;
            foreach (Records recordgo in rlist)
            {
                if (difflvl == "Easy" && recordgo.Diff == "Easy")
                {
                    CreateRecordsTable(current, recordgo, loc_y, count);
                    count++;
                }
                if (difflvl == "Middle" && recordgo.Diff == "Middle")
                {
                    CreateRecordsTable(current, recordgo, loc_y, count);
                    count++;
                }
                if (difflvl == "Hard" && recordgo.Diff == "Hard")
                {
                    CreateRecordsTable(current, recordgo, loc_y, count);
                    count++;
                }
                if (count == 10)
                {
                    break;
                }
            }
        }

        private void CreateRecordsTable(Form current, Records recordgo, int loc_y, int count)
        {
            Label label_count = new Label();
            label_count.Location = new Point(20, loc_y * (count + 1));
            label_count.Text = count + "";
            label_count.Size = new Size(20, 30);
            label_count.Font = new Font("Calibri", 18F);

            Label label_name = new Label();
            label_name.Location = new Point(40, loc_y * (count + 1));
            label_name.Text = recordgo.Name;
            label_name.Size = new Size(280, 30);
            label_name.Font = new Font("Calibri", 18F);

            Label label_time = new Label();
            label_time.Location = new Point(320, loc_y * (count + 1));
            label_time.Text = recordgo.Time_String;
            label_time.Size = new Size(300, 30);
            label_time.Font = new Font("Calibri", 18F);

            current.Controls.Add(label_count);
            current.Controls.Add(label_name);
            current.Controls.Add(label_time);
        }

        private void InitButton(Form current)
        {
            Button button = new Button();
            button.Location = new Point(250,350);
            button.Text = "Назад";
            button.Size = new Size(120, 50);
            button.Font = new Font("Calibri", 18F);
            button.Click += new EventHandler(button_Click);
            current.Controls.Add(button);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Hide();
            main_menu main = new main_menu();
            main.ShowDialog();
            Close();
        }

        private void InitMenu(Form current)
        {
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem main = new ToolStripMenuItem();
            ToolStripMenuItem easy = new ToolStripMenuItem();
            ToolStripMenuItem middle = new ToolStripMenuItem();
            ToolStripMenuItem hard = new ToolStripMenuItem();

            menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            main});
            menu.Location = new System.Drawing.Point(0, 0);
            menu.Size = new System.Drawing.Size(100, 28);
            menu.TabIndex = 0;
            menu.Text = "Выбор уровня сложности";

            main.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            easy,
            middle,
            hard});
            main.Name = "menu";
            main.Size = new System.Drawing.Size(203, 24);
            main.Text = "Выбор уровня сложности";
            main.Click += new System.EventHandler(Menu_main);

            easy.Name = "easy";
            easy.Size = new System.Drawing.Size(224, 26);
            easy.Text = "Легкий";
            easy.Click += new System.EventHandler(Menu_easy);
            // 
            // среднйиToolStripMenuItem
            // 
            middle.Name = "middle";
            middle.Size = new System.Drawing.Size(224, 26);
            middle.Text = "Средний";
            middle.Click += new System.EventHandler(Menu_middle);
            // 
            // сложныйToolStripMenuItem
            // 
            hard.Name = "hard";
            hard.Size = new System.Drawing.Size(224, 26);
            hard.Text = "Сложный";
            hard.Click += new System.EventHandler(Menu_hard);

            current.Controls.Add(menu);
        }

        private void Menu_main(object sender, EventArgs e)
        {

        }

        private void Menu_easy(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            InitRecords(this, "Easy");
            InitButton(this);
            InitMenu(this);
        }
        private void Menu_middle(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            InitRecords(this, "Middle");
            InitButton(this);
            InitMenu(this);
        }
        private void Menu_hard(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            InitRecords(this, "Hard");
            InitButton(this);
            InitMenu(this);
        }

    }
}
