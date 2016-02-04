using System;
using System.IO;
using System.Windows.Forms;

namespace RangeTrainer
{
    public partial class MainForm : Form
    {
        Result _start = new Result();
        private Table _table;
        private Seat _seat0;
        private Seat _seat1;
        private Seat _seat2;
        private Seat _seat3;
        private Seat _seat4;
        private Seat _seat5;
        private Seat _seat6;
        private Seat _seat7;
        private Seat _seat8;

        ActionRange _actionRange = new ActionRange();
        Card[] heroCards = new Card[2];
        bool _heroMove = false;
        bool _result = false;
        double _heroMovesCounter = 0;
        double _heroMistakesCounter = 0;
        double _rightMovePrct = 0;
        private string _scenarioFilePath = "scenario.txt";

        public MainForm()
        {
            StartMenu menu = new StartMenu();
            menu.ShowDialog();
            InitializeComponent();
            
            #region Создаение мест за столом
            _seat0 = new Seat(LabelBox0, LabelSeat0FirstCard, LabelSeat0SecondCard, LabelSeat0BU, labelSeat0BetBox, 0);
            _seat1 = new Seat(LabelBox1, LabelSeat1FirstCard, LabelSeat1SecondCard, LabelSeat1BU, labelSeat1BetBox, 1);
            _seat2 = new Seat(LabelBox2, LabelSeat2FirstCard, LabelSeat2SecondCard, LabelSeat2BU, labelSeat2BetBox, 2);
            _seat3 = new Seat(LabelBox3, LabelSeat3FirstCard, LabelSeat3SecondCard, LabelSeat3BU, labelSeat3BetBox, 3);
            _seat4 = new Seat(LabelBox4, LabelSeat4FirstCard, LabelSeat4SecondCard, LabelSeat4BU, labelSeat4BetBox, 4);
            _seat5 = new Seat(LabelBox5, LabelSeat5FirstCard, LabelSeat5SecondCard, LabelSeat5BU, labelSeat5BetBox, 5);
            _seat6 = new Seat(LabelBox6, LabelSeat6FirstCard, LabelSeat6SecondCard, LabelSeat6BU, labelSeat6BetBox, 6);
            _seat7 = new Seat(LabelBox7, LabelSeat7FirstCard, LabelSeat7SecondCard, LabelSeat7BU, labelSeat7BetBox, 7);
            _seat8 = new Seat(LabelBox8, LabelSeat8FirstCard, LabelSeat8SecondCard, LabelSeat8BU, labelSeat8BetBox, 8);

            _table = new Table(_seat0, _seat1, _seat2, _seat3, _seat4, _seat5,
                _seat6, _seat7, _seat8, ButtonFold, ButtonCall, ButtonRaise);
            #endregion
        }

        private void RunDeal()
        {
            _table.ReadFromFile();
            _actionRange = new ActionRange();
            _actionRange.Shuffle();
            heroCards = _actionRange.DealHeroCards();
            _actionRange.ShowHeroCards(heroCards, LabelSeat4FirstCard, LabelSeat4SecondCard);
        }


        #region Action checking
        private void CheckMove(bool heroMove, bool result)
        {
            _heroMovesCounter += 1;

            if (heroMove != result)
            {
                LabelWrongAction.Text = "W R O N G";
                MessageBox.Show("what the fuck ?", "what the fuck ?");
            }
            else
            {
                _rightMovePrct += 1;
            }

            LabelWrongAction.Text = "";
        }

        private void ButtonFold_Click(object sender, EventArgs e)
        {
            if (_table.ConstructerMode == false)
            {
                LabelWrongAction.Text = "";
                _heroMove = false;

                _result = _actionRange.Raise.CheckHeroAction(heroCards);
                CheckMove(_heroMove, _result);

                _result = _actionRange.Call.CheckHeroAction(heroCards);
                CheckMove(_heroMove, _result);

                RunDeal();
            }
            else
            {
                for (int i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Index == _table.ActivePlayerIndex)
                    {
                        _table[i].ShowAsFolded();
                        _table.SaveAction(i);
                        _table.NextActivePlayer();
                        _table.Remove(_table[i].Index);
                    }
                }
                _table.RunConstructerMode();
            }
        }

        private void ButtonCall_Click(object sender, EventArgs e)
        {
            if (_table.ConstructerMode == false)
            {
                LabelWrongAction.Text = "";
                _heroMove = true;
                _result = _actionRange.Call.CheckHeroAction(heroCards);
                CheckMove(_heroMove, _result);

                RunDeal();
            }
            else
            {
                for (int i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Index == _table.ActivePlayerIndex)
                    {
                        _table[i].Bet(Convert.ToDouble(textBoxBetSize.Text));
                        _table.SaveAction(i);
                    }
                }
                _table.NextActivePlayer();
                _table.RunConstructerMode();
            }
        }

        private void ButtonRaise_Click(object sender, EventArgs e)
        {
            if (_table.ConstructerMode == false)
            {
                LabelWrongAction.Text = "";
                _heroMove = true;
                _result = _actionRange.Raise.CheckHeroAction(heroCards);
                CheckMove(_heroMove, _result);

                RunDeal();
            }
            else
            {
                for (int i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Index == _table.ActivePlayerIndex)
                    {
                        _table[i].Bet(Convert.ToDouble(textBoxBetSize.Text));
                        _table.SaveAction(i);
                    }
                }
                _table.NextActivePlayer();
                _table.RunConstructerMode();
            }
        }

        #endregion

        //Hotkeys for buttons

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.NumPad5))
            {
                ButtonFold.PerformClick();
                return true;
            }
            if (keyData == (Keys.NumPad4))
            {
                ButtonCall.PerformClick();
                return true;
            }
            if (keyData == (Keys.NumPad6))
            {
                ButtonRaise.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonDeal_Click(object sender, EventArgs e)
        {
            if (_table.ConstructerMode == true)
            {
                _table.CloseConstructerMode();

                RangeSettingMenu menu = new RangeSettingMenu();
                menu.ShowDialog();

                buttonDeal.Visible = false;
            }
            _start.Start();
            buttonDeal.Visible = false;
            RunDeal();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            _start.Finish((int)(_rightMovePrct * 100 / _heroMovesCounter));
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                var source = File.ReadAllText("tmp.txt");
                File.WriteAllText(saveFile.FileName, source);
            }
        }
    }

}
