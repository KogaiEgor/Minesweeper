using Minesweeper.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Minesweeper
{
    public partial class result : Form
    {
        private TextBox input = new TextBox();
        public static Label label1 = new Label();
        public static Label label2 = new Label();
        public static string difflvl;
        private int button_y;
        private string _name;
        public int time;
        public result()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Init()
        {
            InitializeComponent();
            InitLabel(this);
            InitButton(this);
            if (label2.Text == "Победа")
            {
                InitTextbox(this);
            }
        }

        public static void InitLabel(Form current)
        {
            label1.Location = new Point(170, 100);
            label1.Font = new Font("Calibri", 18F);
            label1.Size = new Size(220, 40);
            label2.Location = new Point(210, 60);
            label2.Size = new Size(220, 40);
            label2.Font = new Font("Calibri", 18F);
            current.Controls.Add(label1);
            current.Controls.Add(label2);
        }
        public void InitButton(Form current)
        {
            if (label2.Text == "Поражение")
            {
                button_y = 140;
            } else
            {
                button_y = 200;
            }
            Button restart_button = new Button();
            restart_button.Location = new Point(220, button_y);
            restart_button.Size = new Size(120, 80);
            restart_button.Text = "Играть заново";
            restart_button.Font = new Font("Calibri", 16F);
            restart_button.Click += new EventHandler(restart_button_Click);
            
            Button close_button = new Button();
            close_button.Location = new Point(350, button_y);
            close_button.Size = new Size(120, 80);
            close_button.Text = "Выход";
            close_button.Font = new Font("Calibri", 16F);
            close_button.Click += new EventHandler(close_button_Click);

            Button choice_button = new Button();
            choice_button.Location = new Point(90, button_y);
            choice_button.Size = new Size(120, 80);
            choice_button.Text = "Главное меню";
            choice_button.Font = new Font("Calibri", 16F);
            choice_button.Click += new EventHandler(choise_button_Click);

            current.Controls.Add(restart_button);
            current.Controls.Add(close_button);
            current.Controls.Add(choice_button);
        }
        
        private void InitTextbox(Form current)
        {
            Label label = new Label();
            label.Text = "Введите имя";
            label.Location = new Point(100, 150);
            label.Font = new Font("Calibri", 18F);
            label.Size = new Size(150, 50);
            input.Location = new Point(260, 150);
            input.Size = new Size(180, 70);
            input.Font = new Font("Calibri", 18F);
            current.Controls.Add(label);
            current.Controls.Add(input);
        }

        public void AddRecord(Form current)
        {
            string filename = "Record.bin";
            LinkedList<Records> rlist = new LinkedList<Records>();
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
            Records record = new Records();
            record.Name = _name;
            record.Diff = difflvl;
            record.Time = time;
            record.Time_String = Gameplay.time_str;
            rlist.AddLast(record);
            rlist = new LinkedList<Records>(rlist.OrderBy(value => value.Time));
            if (rlist.Count > 250)
            {
                rlist.RemoveLast();
            }

            binaryReader.Close();

            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                foreach (Records recordgo in rlist)
                {
                    binaryWriter.Write(recordgo.Name);
                    binaryWriter.Write(recordgo.Diff);
                    binaryWriter.Write(recordgo.Time);
                    binaryWriter.Write(recordgo.Time_String);
                }
                binaryWriter.Close();
            }

        }

        private void restart_button_Click(object sender, EventArgs e)
        {
            Hide();
            if (label2.Text == "Победа")
            {
                _name = input.Text;
                AddRecord(this);
            }
            if (difflvl == "Easy")
            {
                easy game = new easy();
                game.ShowDialog();
            }
            else if (difflvl == "Middle")
            {
                middle game = new middle();
                game.ShowDialog();
            }
            else if (difflvl == "Hard")
            {
                hard game = new hard();
                game.ShowDialog();
            }
            Close();
        }

        private void choise_button_Click(object sender, EventArgs e)
        {
            Hide();
            if (label2.Text == "Победа")
            {
                _name = input.Text;
                AddRecord(this);
            }
            main_menu menu = new main_menu();
            Close();
            menu.ShowDialog();
        }


        private void close_button_Click(object sender, EventArgs e)
        {
            
            if (label2.Text == "Победа")
            {
                _name = input.Text;
                AddRecord(this);
            }
            Close();
        }

        private void result_Load(object sender, EventArgs e)
        {

        }
    }
}
