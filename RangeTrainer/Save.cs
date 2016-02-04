using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RangeTrainer
{
    class Save
    {
        private Table _table;
        private ActionRange _range;

        public Save(Table table, ActionRange range, string path)
        {
            _table = table;
            _range = range;
            ToFile(path);
        }

        private void SavePossitionsToFile(string path)
        {
            // сохраняем список активных мест за столом + стек каждого активного места
            File.WriteAllText(path, "<Seats>");
            for (int i = 0; i < _table.Length; i++)
            {
                File.AppendAllText(path, _table[i].Index.ToString());
                File.AppendAllText(path, " ");
                File.AppendAllText(path, _table[i].Box.Text.ToString());
                if (i < _table.Length-1)
                {
                    File.AppendAllText(path, ",");
                }
            }
            File.AppendAllText(path, "</Seats>");

            // сохраняем позицию баттона
            File.AppendAllText(path, "<BU>");
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i].Button.Visible == true)
                {
                    File.AppendAllText(path, _table[i].Index.ToString());
                }
            }
            File.AppendAllText(path, "</BU>");

        }

        private void SaveRangesToFile(string path)
        {
            // сохраняем диапазон рейза
            File.AppendAllText(path, "\r" + "\r");
            File.AppendAllText(path, "<Raise>");
            var raiseRange = File.ReadAllText("ranges\\raise.txt");
            File.AppendAllText(path, raiseRange);
            File.AppendAllText(path, "</Raise>");

            // сохраняем диапазон колла
            File.AppendAllText(path, "\r");
            File.AppendAllText(path, "<Call>");
            var callRange = File.ReadAllText("ranges\\call.txt");
            File.AppendAllText(path, callRange);
            File.AppendAllText(path, "</Call>");
        }

        public void ToFile(string path)
        {
            SavePossitionsToFile(path);
            SaveRangesToFile(path);
        }
    }
}
