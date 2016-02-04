using System;
using System.IO;
using System.Windows.Forms;

namespace RangeTrainer
{
    public partial class StartMenu : Form
    {
        private MenuControl _ep1;
        private MenuControl _ep2;
        private MenuControl _mp1;
        private MenuControl _mp2;
        private MenuControl _mp3;
        private MenuControl _co;
        private MenuControl _bu;
        private MenuControl _sb;
        private MenuControl _bb;
        private MenuControlDock _myControls;
        private string _tempFilePath = "tmp.txt";
        private string _scenarioPath = "scenario.txt";
        private string _tempScenarioPool = "tmp.txt";
        private bool _scenarioModeOn = false;

        #region Constructers
        public StartMenu()
        {
            InitializeComponent();

            File.WriteAllText(_scenarioPath, "");

            _ep1 = new MenuControl(checkBoxEP1, textBoxEP1, radioButtonEP1, 0);
            _ep2 = new MenuControl(checkBoxEP2, textBoxEP2, radioButtonEP2, 1);
            _mp1 = new MenuControl(checkBoxMP1, textBoxMP1, radioButtonMP1, 2);
            _mp2 = new MenuControl(checkBoxMP2, textBoxMP2, radioButtonMP2, 3);
            _mp3 = new MenuControl(checkBoxMP3, textBoxMP3, radioButtonMP3, 4);
            _co = new MenuControl(checkBoxCO, textBoxCO, radioButtonCO, 5);
            _bu = new MenuControl(checkBoxBU, textBoxBU, radioButtonBU, 6);
            _sb = new MenuControl(checkBoxSB, textBoxSB, radioButtonSB, 7);
            _bb = new MenuControl(checkBoxBB, textBoxBB, radioButtonBB, 8);

            _myControls = new MenuControlDock(_ep1, _ep2, _mp1, _mp2, _mp3, _co, _bu, _sb, _bb);
        }

        #endregion

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (_scenarioModeOn == true)
            {
                var index = _tempScenarioPool.LastIndexOf(",");
                if (index > 0)
                {
                    _tempScenarioPool = _tempScenarioPool.Remove(index);
                }

                File.WriteAllText(_scenarioPath, _tempScenarioPool);
                this.Close();
            }
            else
            {
                _myControls.Save();
            }
            this.Close();
        }

        private void EnableButtonStart()
        {
            var _buttonStartTurn = _myControls.IsHeroSet();
            if (_buttonStartTurn == true)
            {
                buttonStart.Enabled = true;
            }
            else
            {
                buttonStart.Enabled = false;
            }
        }

        private void checkBoxEP1_CheckedChanged(object sender, EventArgs e)
        {
            _myControls.IsChecked();
            EnableButtonStart();
        }

        private void radioButtonEP1_CheckedChanged(object sender, EventArgs e)
        {
            EnableButtonStart();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            var loadFile = new OpenFileDialog();
            if (loadFile.ShowDialog() == DialogResult.OK)
            {
                var swap = File.ReadAllText(loadFile.FileName);
                File.WriteAllText(_tempFilePath, swap);
                this.Hide();
            }
        }

        private void buttonScenario_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _scenarioModeOn = true;
                buttonStart.Enabled = true;
                if (_tempScenarioPool == _tempFilePath)
                {
                    _tempScenarioPool = ofd.FileName;
                    _tempScenarioPool += ",";
                }
                else
                {
                    _tempScenarioPool += ofd.FileName;
                    _tempScenarioPool += ",";
                }
            }
        }
    }
}
