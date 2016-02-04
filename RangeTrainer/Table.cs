using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RangeTrainer
{
    class Table
    {
        private Seat[] _table = new Seat[0];
        private Seat[] _allSeats = new Seat[9];
        private Button _buttonFold;
        private Button _buttonRaise;
        private Button _buttonCall;
        private string _temp = "tmp.txt";
        private string _scenarioPath = "scenario.txt";
        private string _handHistory = "<Constructer>false</Constructer><Preflop>";
        private int _length = 0;
        private int _buIndex;
        private int _sbIndex;
        private int _bbIndex;
        private bool _constructerMode = false;
        private int _activePlayerIndex;
        private int deffaultHeroIndex = 4;
        private string[] _savedFilesPool = new string[0];


        #region Properties
        public int Length
        {
            get
            {
                return _length;
            }
        }

        public bool ConstructerMode
        {
            get { return _constructerMode; }
            set { _constructerMode = value; }
        }

        public Seat this[int index]
        {
            get
            {
                return _table[index];
            }
        }

        public int ActivePlayerIndex
        {
            get { return _activePlayerIndex; }
            set { _activePlayerIndex = value; }
        }
        #endregion

        #region Constructers
        public Table(Seat first, Seat second, Seat third, Seat fourth,
            Seat fifth, Seat sixth, Seat seventh, Seat eighth, Seat nineth,
            Button fold, Button call, Button raise)
        {
            _allSeats[0] = first;
            _allSeats[1] = second;
            _allSeats[2] = third;
            _allSeats[3] = fourth;
            _allSeats[4] = fifth;
            _allSeats[5] = sixth;
            _allSeats[6] = seventh;
            _allSeats[7] = eighth;
            _allSeats[8] = nineth;

            _buttonFold = fold;
            _buttonCall = call;
            _buttonRaise = raise;

            ReadFromFile();
            DeffineConstructersStartIndex();
            RunConstructerMode();

        }
        #endregion

        public void Add(Seat seat)
        {
            Array.Resize(ref _table, _table.Length + 1);
            _table[_length] = seat;
            _length += 1;

            Sort(_table);
        }

        public void Add(int index, int stack)
        {
            for (int i = 0; i < _allSeats.Length; i++)
            {
                if (_allSeats[i].Index == index)
                {
                    Array.Resize(ref _table, _table.Length + 1);
                    _table[_length] = _allSeats[i];
                    _table[_length].Box.Text = stack.ToString();
                    _length += 1;
                }
            }

        }

        public void Remove(int index)
        {
            Seat[] temp = new Seat[0];
            for (int i = 0, j = 0; i < _table.Length; i++)
            {
                if (_table[i].IsFolded != true)
                {
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[j] = _table[i];
                    j++;
                }
            }

            _table = new Seat[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                _table[i] = temp[i];
            }

            _length = _table.Length;
        }

        private void Sort(Seat[] table)
        {
            Seat[] swap = new Seat[1];
            for (int i = 1; i < table.Length; i++)
            {
                var j = i;
                while (j > 0 && table[j].Index < table[j - 1].Index)
                {
                    swap[0] = table[j];
                    table[j] = table[j - 1];
                    table[j - 1] = swap[0];

                    j--;
                }
            }
        }

        private int ConvertIndexToPossition(int index)
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (index == _table[i].Index)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Hide()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i].Hide();
            }
        }

        public void Show()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i].Show();
            }
        }

        public void SetButton(int index)
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (index == _table[i].Index)
                {
                    _table[i].Button.Visible = true;
                }
                else
                {
                    _table[i].Button.Visible = false;
                }
            }
        }

        private void ClearAllBets()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i].BetBox.Text = "";
                _table[i].BetBox.Visible = false;
            }
        }

        private void PostBlinds(Seat[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Button.Visible = true)
                {
                    if (i == table.Length - 2)
                    {
                        table[i + 1].BetBox.Visible = true;
                        table[i + 1].BetBox.Text = "0.5";

                        table[0].BetBox.Visible = true;
                        table[0].BetBox.Text = "1";
                    }
                    else if (i == table.Length - 1)
                    {
                        table[0].BetBox.Visible = true;
                        table[0].BetBox.Text = "0.5";

                        table[1].BetBox.Visible = true;
                        table[1].BetBox.Text = "1";
                    }
                    else
                    {
                        table[i + 1].BetBox.Visible = true;
                        table[i + 1].BetBox.Text = "0.5";

                        table[i + 2].BetBox.Visible = true;
                        table[i + 2].BetBox.Text = "1";
                    }
                }
            }
        }

        public void MakeActive(int index)
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (index == _table[i].Index)
                {
                    _table[i].ShowAsActive();
                    _activePlayerIndex = _table[i].Index;
                }
                else
                {
                    _table[i].Box.BackColor = Color.FromArgb(40, 40, 40);
                }
            }
        }

        #region Methods for loading from file
        public void ReadFromFile()
        {
            Hide();
            _table = new Seat[0];
            _length = 0;
            ClearAllBets();

            ReadScenarioFromFile();

            var checking = File.ReadAllText(_temp);
            if (checking.Length > 0)
            {
                var seats = Read(_temp, "<Seats>", "</Seats>");
                ExtractNumbers(seats);
                Show();
                SetBU();
                SetSB();
                SetBB();
                DeffineConstructerMode();
                SetPlayersActions();
            }
        }

        public void ReadScenarioFromFile()
        {
            _savedFilesPool = new string[0];
            var source = File.ReadAllText(_scenarioPath);
            if (source.Length > 0)
            {
                ExtractScenario(source);
                RandomizeScenario();
            }
        }

        private void ExtractScenario(string source)
        {
            string[] myScenarios = source.Split(',');
            for (int i = 0; i < myScenarios.Length; i++)
            {
                Array.Resize(ref _savedFilesPool, _savedFilesPool.Length + 1);
                _savedFilesPool[i] = myScenarios[i];
            }
        }

        private void RandomizeScenario()
        {
            Random rand = new Random();
            int seed = rand.Next(_savedFilesPool.Length);
            var source = File.ReadAllText(_savedFilesPool[seed]);
            File.WriteAllText(_temp, source);
        }

        private string Read(string path, string startTag, string finishTag)
        {
            var source = File.ReadAllText(path);
            var startIndex = source.IndexOf(startTag);
            var finishIndex = source.IndexOf(finishTag);

            startIndex += startTag.Length;
            var result = source.Substring(startIndex, finishIndex - startIndex);
            return result;
        }

        private string ReadPlayersActions(string path, string startTag, string finishTag)
        {
            var source = File.ReadAllText(path);
            var startIndex = source.IndexOf(startTag);
            var finishIndex = source.IndexOf(finishTag);

            if (startIndex < 0 || finishIndex < 0)
            {
                return "";
            }
            else
            {
                startIndex += startTag.Length;
                var result = source.Substring(startIndex, finishIndex - startIndex);
                return result;
            }
        }

        private void ExtractNumbers(string source)
        {
            if (source.Length > 0)
            {
                string[] tempSplit = source.Split(',');
                string[] temp;

                for (int i = 0; i < tempSplit.Length; i++)
                {
                    temp = tempSplit[i].Split();
                    var first = Convert.ToInt32(temp[0]);
                    var second = Convert.ToInt32(temp[1]);
                    Add(first, second);
                }
                Sort(_table);
            }
        }

        private void DeffineConstructerMode()
        {
            var mode = Read(_temp, "<Constructer>", "</Constructer>");
            if (mode == "true")
            {
                _constructerMode = true;
            }
            else
            {
                _constructerMode = false;
            }
        }

        private int SetBU()
        {
            var buttonPossition = Read(_temp, "<BU>", "</BU>");
            if (buttonPossition.Length > 0)
            {
                for (int i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Index == Convert.ToInt32(buttonPossition))
                    {
                        _table[i].ShowAsButton();
                        _buIndex = _table[i].Index;
                        return i;
                    }
                }
            }

            return -1;
        }

        private int SetSB()
        {
            var sbPossition = Read(_temp, "<SB>", "</SB>");
            if (sbPossition.Length > 0)
            {
                for (int i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Index == Convert.ToInt32(sbPossition))
                    {
                        _table[i].PostSmallBlind();
                        _sbIndex = _table[i].Index;
                        return i;
                    }
                }
            }

            return -1;
        }

        private int SetBB()
        {
            var bbPossition = Read(_temp, "<BB>", "</BB>");
            if (bbPossition.Length > 0)
            {
                for (int i = 0; i < _table.Length; i++)
                {
                    if (_table[i].Index == Convert.ToInt32(bbPossition))
                    {
                        _table[i].PostBigBlind();
                        _bbIndex = _table[i].Index;
                        return i;
                    }
                }
            }

            return -1;
        }

        private void SetPlayersActions()
        {
            var source = ReadPlayersActions(_temp, "<Preflop>", "</Preflop>");

            if (source != "")
            {
                string[] tempSplit = source.Split(',');
                string[] temp;

                for (int i = 0; i < tempSplit.Length; i++)
                {
                    temp = tempSplit[i].Split();
                    var first = Convert.ToInt32(temp[0]);
                    var second = Convert.ToDouble(temp[1]);
                    ShowLoadedAction(first, second);
                }
            }
        }

        private void ShowLoadedAction(int seatNumber, double betAmount)
        {
            if (betAmount == 0.5 && seatNumber != deffaultHeroIndex)
            {
                var index = ConvertIndexToPossition(seatNumber);
                _table[index].ShowAsFolded();
            }
            else if (betAmount == 0)
            {
                var index = ConvertIndexToPossition(seatNumber);
                _table[index].ShowAsFolded();
            }
            else
            {
                var index = ConvertIndexToPossition(seatNumber);
                _table[index].Bet(betAmount);
            }
        }

        #endregion

        #region Methods for Constructing player's action
        public void RunConstructerMode()
        {
            if (_constructerMode == true)
            {
                var startIndex = _activePlayerIndex;
                MakeActive(startIndex);
            }
        }

        public int DeffineConstructersStartIndex()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i].Index == _bbIndex)
                {
                    if (i == _table.Length - 1)
                    {
                        _activePlayerIndex = _table[0].Index;
                        return _activePlayerIndex;
                    }
                    else
                    {
                        var j = i + 1;
                        _activePlayerIndex = _table[j].Index;
                        return _activePlayerIndex;
                    }
                }
            }
            return _activePlayerIndex;
        }

        public int NextActivePlayer()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i].Index == _activePlayerIndex)
                {
                    if (i == _table.Length - 1)
                    {
                        _activePlayerIndex = _table[0].Index;
                        return _activePlayerIndex;
                    }
                    else
                    {
                        var j = i + 1;
                        _activePlayerIndex = _table[j].Index;
                        return _activePlayerIndex;
                    }
                }
            }
            return _activePlayerIndex;
        }

        public void SaveAction(int index)
        {
            _handHistory += _table[index].Index;
            _handHistory += " ";

            if (_table[index].BetBox.Text == "")
            {
                _handHistory += "0";
                _handHistory += ",";
            }
            else
            {
                _handHistory += _table[index].BetBox.Text;
                _handHistory += ",";
            }
        }

        private void RemoveConstructerTagInFile()
        {
            var temp = File.ReadAllText(_temp);
            temp = temp.Remove(temp.LastIndexOf("<Constructer>"));
            File.WriteAllText(_temp, temp);
        }

        public void CloseConstructerMode()
        {
            RemoveConstructerTagInFile();
            _constructerMode = false;

            var index = _handHistory.LastIndexOf(",");
            if (index > -1)
            {
                _handHistory = _handHistory.Remove(_handHistory.LastIndexOf(","));
                _handHistory += "</Preflop>";
                File.AppendAllText(_temp, _handHistory);
            }

        }
        #endregion
    }
}
