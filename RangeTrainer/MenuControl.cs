using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RangeTrainer
{
    class MenuControl
    {
        private CheckBox _checkBoxIsActive;
        private TextBox _textBoxStack;
        private RadioButton _radioButtonIsHero;
        private int _seatIndex;

        #region Constructers
        public MenuControl(CheckBox isActive, TextBox stack, RadioButton isHero, int index)
        {
            _checkBoxIsActive = isActive;
            _textBoxStack = stack;
            _radioButtonIsHero = isHero;
            _seatIndex = index;

            _checkBoxIsActive.Checked = false;
            _textBoxStack.Text = "100";
        }
        #endregion

        #region Properties
        public int SeatIndex
        {
            get { return _seatIndex; }
            set { _seatIndex = value; }
        }

        public RadioButton IsHero
        {
            get { return _radioButtonIsHero; }
            set { _radioButtonIsHero = value; }
        }

        public TextBox Stack
        {
            get { return _textBoxStack; }
            set { _textBoxStack = value; }
        }

        public CheckBox IsActive
        {
            get { return _checkBoxIsActive; }
            set { _checkBoxIsActive = value; }
        }
        #endregion

        public void IsChecked()
        {
            if (_checkBoxIsActive.Checked == true)
            {
                _textBoxStack.Enabled = true;
                _radioButtonIsHero.Enabled = true;
            }
            else
            {
                _textBoxStack.Enabled = false;
                _radioButtonIsHero.Checked = false;
                _radioButtonIsHero.Enabled = false;
            }
        }
    }
}
