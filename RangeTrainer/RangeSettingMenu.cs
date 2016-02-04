using System;
using System.IO;
using System.Windows.Forms;

namespace RangeTrainer
{
    public partial class RangeSettingMenu : Form
    {
        private string[] _savedFiles = new string[0];
        private int _savedFilesCounter = 0;
        private string _rangesSaveFile = "tmp.txt";
        public RangeSettingMenu()
        {
            InitializeComponent();

            File.WriteAllText("scenario.txt", "");

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var raiseRange = "<Raise>";
            raiseRange += textBoxRaiseRange.Text;
            raiseRange += "</Raise>";

            var callRange = "<Call>";
            callRange += textBoxCallRange.Text;
            callRange += "</Call>";

            File.AppendAllText(_rangesSaveFile, raiseRange);
            File.AppendAllText(_rangesSaveFile, callRange);

            for (int i = 0; i < _savedFiles.Length; i++)
            {
                File.AppendAllText("scenario.txt", _savedFiles[i] + "\r");
            }

            this.Hide();
        }

    }
}
