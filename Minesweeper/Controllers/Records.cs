using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Controllers
{
    struct Records
    {
        private string _name;
        private string _diff;
        private int _time;
        private string _time_string;

        public Records(string name, string diff, int time, string time_string)
        {
            _name = name;   
            _diff = diff;
            _time = time;
            _time_string = time_string;
        }

        public override string ToString()
        {
            return $"    {_name}   {_time_string}   {_diff}";
        }

        public string Time_String
        {
            get { return _time_string; }
            set { _time_string = value; }
        }
        public string Name
        {
            get { return _name; }
            set 
            { 
                if (value == "")
                {
                    _name = "noname";
                } else
                {
                    _name = value; 
                }
            }
        }

        public string Diff
        {
            get { return _diff; }
            set { _diff = value; }
        }

        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
            }
        }
    }
}
