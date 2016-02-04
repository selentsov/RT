using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RangeTrainer
{
    class MenuControlDock
    {
        MenuControl[] _myControls = new MenuControl[9];
        private const int _deffaultHeroPos = 4;
        private int _currentHeroPos;

        private string _savePath = "tmp.txt";
        private string _saveSource ="";
        private string _openTag = "<Seats>";
        private string _closeTag = "</Seats>";
        private string _seporator = ",";
        

        #region Constructors
        public MenuControlDock(MenuControl first, MenuControl second, MenuControl third, MenuControl fourth, 
            MenuControl fifth, MenuControl sixth, MenuControl seventh, MenuControl eighth, MenuControl nineth)
        {
            _myControls[0] = first;
            _myControls[1] = second;
            _myControls[2] = third;
            _myControls[3] = fourth;
            _myControls[4] = fifth;
            _myControls[5] = sixth;
            _myControls[6] = seventh;
            _myControls[7] = eighth;
            _myControls[8] = nineth;
        }
        
        #endregion

        #region Methods for Reindex
        private void Reindex()
        {
            DeffineCurrentHeroPos();

            if (_currentHeroPos > _deffaultHeroPos)
            {
                MoveIndexDown();
            }
            else if (_currentHeroPos < _deffaultHeroPos)
            {
                MoveIndexUp();
            }

            File.WriteAllText("debug.txt", _currentHeroPos.ToString());
        }

        private int DeffineCurrentHeroPos()
        {
            for (int i = 0; i < _myControls.Length; i++)
            {
                if (_myControls[i].IsHero.Checked == true)
                {
                    _currentHeroPos = _myControls[i].SeatIndex;
                    return _currentHeroPos;
                }
            }

            return _currentHeroPos;
        }

        private void MoveIndexUp()
        {
            var summator = _deffaultHeroPos - _currentHeroPos;
            for (int i = 0; i < _myControls.Length; i++)
            {
                for (int j = i, t = 0; t < summator; t++)
                {
                    if (j == _myControls.Length - 1)
                    {
                        j = 0;
                        _myControls[i].SeatIndex = j;
                    }
                    else
                    {
                        j += 1;
                        _myControls[i].SeatIndex = j;
                    }
                }
            }
        }

        private void MoveIndexDown()
        {
            var moveIndex = _currentHeroPos - _deffaultHeroPos;
            for (int i = 0; i < _myControls.Length; i++)
            {
                for (int j = i, t = 0; t < moveIndex; t++)
                {
                    if (j == 0)
                    {
                        j = _myControls.Length - 1;
                        _myControls[i].SeatIndex = j;
                    }
                    else
                    {
                        j -= 1;
                        _myControls[i].SeatIndex = j;
                    }
                }
            }
        }
        
        #endregion

        public void IsChecked()
        {
            for (int i = 0; i < _myControls.Length; i++)
            {
                _myControls[i].IsChecked();
            }
        }

        public bool IsHeroSet()
        {
            for (int i = 0; i < _myControls.Length; i++)
            {
                if (_myControls[i].IsHero.Checked==true)
                {
                    return true;
                }
            }
            return false;
        }

        #region Methods for saving config
        public void Save()
        {
            Reindex();
            SavePossitions();
            SaveBuLocation();
            SaveSBLocation();
            SaveBBLocation();
            SetConstructerMode();
        }

        private void SavePossitions()
        {
            _saveSource += _openTag;

            for (int i = 0; i < _myControls.Length; i++)
            {
                if (_myControls[i].IsActive.Checked == true)
                {
                    _saveSource += _myControls[i].SeatIndex.ToString();
                    _saveSource += " ";
                    _saveSource += _myControls[i].Stack.Text;
                    _saveSource += _seporator;
                }
            }

            _saveSource = _saveSource.Remove(_saveSource.LastIndexOf(_seporator));
            _saveSource += _closeTag;

            File.WriteAllText(_savePath, _saveSource);
        }

        private void SaveBuLocation()
        {
            for (int i = 0; i < _myControls.Length; i++)
            {
                if (_myControls[i].IsActive.Text == "BU")
                {
                    File.AppendAllText(_savePath, "<BU>" + _myControls[i].SeatIndex.ToString() + "</BU>");
                }
            }
        }

        private void SaveSBLocation()
        {
            for (int i = 0; i < _myControls.Length; i++)
            {
                if (_myControls[i].IsActive.Text == "SB")
                {
                    File.AppendAllText(_savePath, "<SB>" + _myControls[i].SeatIndex.ToString() + "</SB>");
                }
            }
        }

        private void SaveBBLocation()
        {
            for (int i = 0; i < _myControls.Length; i++)
            {
                if (_myControls[i].IsActive.Text == "BB")
                {
                    File.AppendAllText(_savePath, "<BB>" + _myControls[i].SeatIndex.ToString() + "</BB>");
                }
            }
        }

        private void SetConstructerMode()
        {
            File.AppendAllText(_savePath, "<Constructer>" + "true" + "</Constructer>" + "<Preflop>");
        }
        #endregion
    }
}