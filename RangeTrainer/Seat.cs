using System.Drawing;
using System.Windows.Forms;

namespace RangeTrainer
{
    class Seat
    {
        private Label _box;
        private Label _firstCard;
        private Label _secondCard;
        private Label _button;
        private Label _betBox;
        private Player _player;
        private int _index;

        private bool _isActive;
        private bool _isFolded;

        private string _bigBlind = "1 bb";
        private string _smallBlind = "0.5 bb";

        #region Constructors
        public Seat(Label box, Label firstCard,
            Label secondCard, Label button, Label betBox, int index)
        {
            _box = box;
            _firstCard = firstCard;
            _secondCard = secondCard;
            _button = button;
            _betBox = betBox;
            _index = index;
            _isActive = false;
        }
        #endregion

        #region Properties

        public Label BetBox
        {
            get { return _betBox; }
            set { _betBox = value; }
        }
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public Label Box
        {
            get { return _box; }
        }

        public Label Button
        {
            get { return _button; }
        }

        public bool IsFolded
        {
            get { return _isFolded; }
            set { _isFolded = value; }
        }

        #endregion

        //public void PlacePlayer(Player player)
        //{
        //    _player = player;
        //    _box.Text = _player.Stack.ToString();
        //    _firstCard.Text = _player.FirstCard.ToString();
        //    _secondCard.Text = _player.SecondCard.ToString();
        //    _button.Visible = false;
        //    _betBox.Visible = false;
        //}

        public void Bet(double betAmount)
        {
            _betBox.Text = betAmount.ToString();
            _betBox.Visible = true;
        }

        public void Call(double callAmount)
        {
            Bet(callAmount);
        }

        public void Fold()
        {
            ShowAsFolded();
        }

        public void PostBigBlind()
        {
            Bet(1);
        }

        public void PostSmallBlind()
        {
            Bet(0.5);
        }

        public void Hide()
        {
            _box.Visible = false;
            _firstCard.Visible = false;
            _secondCard.Visible = false;
            _button.Visible = false;
            _betBox.Visible = false;
        }

        public void Show()
        {
            _box.Visible = true;
            _box.BackColor = Color.FromArgb(40, 40, 40);
            _box.ForeColor = Color.Cornsilk;
            //_box.BorderStyle = BorderStyle.FixedSingle;
            _firstCard.Visible = true;
            _secondCard.Visible = true;
            _button.Visible = false;
            _betBox.Visible = false;
        }

        public void ShowAsButton()
        {
            _box.Visible = true;
            _firstCard.Visible = true;
            _secondCard.Visible = true;
            _button.Visible = true;
            _betBox.Visible = false;
        }
         
        public void ShowAsFolded()
        {
            _firstCard.Visible = false;
            _secondCard.Visible = false;
            _box.ForeColor = Color.FromArgb(80, 80, 80);
            _box.BackColor = Color.FromArgb(30, 30, 30);
            _box.BorderStyle = BorderStyle.None;
            _isFolded = true;
        }

        public void ShowAsActive()
        {
            _isActive = true;
            _box.BackColor = Color.FromArgb(60, 60, 60);
        }
    }
}
