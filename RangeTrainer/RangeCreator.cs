/*
 * Данный класс считывает указанный пользователем ренж (формат Flopzill'ы)из файла
 * и создает массив, который заполняет всеми комбинациями входящими в считанный ренж.
*/
using System.IO;

namespace RangeTrainer
{
    internal class RangeCreator : Deck
    {
        public Combo[] _convertedRange = new Combo[1326];
        private Card[] _firstCardArray = new Card[4];
        private Card[] _secondCardArray = new Card[4];
        private string _range = "";
        int _combosCounter = 0;

        #region Properties
        public string Range
        {
            get { return _range; }
        }

        public int CombosCounter
        {
            get { return _combosCounter; }
            set { _combosCounter = value; }
        }

        public Combo this[int index]
        {
            get { return _convertedRange[index]; }
            set { _convertedRange[index] = value; }
        }

        #endregion

        #region Constructors
        public RangeCreator(string sourceRange)
        {
            //FormatFile(path);
            FormatString(sourceRange);
        }

        #endregion

        // Метод возвращающий строку с перечнем всех комб входящих в спектр (для теста ?)
        public void WriteRangetoFile(string path)
        {
            var combos = "";
            for (int i = 0; i < _combosCounter; i++)
            {
                combos += _convertedRange[i].ComboToString();
                combos += ",";
            }

            File.WriteAllText(path, combos);
        }

        #region Блок кода создающий массив с комбами входящими в ренж
        // Находим в колоде карты всех мастей с данным номиналом 
        // и копируем их в отдельный массив
        private void PushHandInstances(char card, Card[] array)
        {
            for (int i = 0, j = 0; j < array.Length; i++)
            {
                if (_deck[i].CardFace == card)
                {
                    array[j] = _deck[i];
                    j++;
                }
            }
        }

        // Конвертирует короткую запись карманных пар в полный
        // перечень комбинаций данной карманной пары
        private void ConvertPocketPairs(Card[] combos)
        {
            Combo temp = new Combo();

            for (int i = 0; i < combos.Length - 1; i++)
            {
                for (int j = i + 1; j < combos.Length; j++, _combosCounter++)
                {
                    temp = new Combo();
                    temp.CreateCombo(combos[i], combos[j]);
                    _convertedRange[_combosCounter] = temp;

                }
            }
        }

        // Конвертирует короткую запись не парного покета в полный
        // перечень комбинаций данной карманной пары
        private void ConvertUnpaired(Card[] firstCardArray, Card[] secondCardArray)
        {
            Combo temp = new Combo();

            for (int i = 0; i < firstCardArray.Length; i++)
            {
                for (int j = 0; j < secondCardArray.Length; j++, _combosCounter++)
                {
                    temp = new Combo();
                    temp.CreateCombo(firstCardArray[i], secondCardArray[j]);
                    _convertedRange[_combosCounter] = temp;
                }
            }
        }

        private void ConvertSuitedHand(Card[] firstCardArray, Card[] secondCardArray)
        {
            Combo temp = new Combo();

            for (int i = 0; i < firstCardArray.Length; i++)
            {
                for (int j = 0; j < secondCardArray.Length; j++)
                {
                    if (secondCardArray[j].CardSuit == firstCardArray[i].CardSuit)
                    {
                        temp = new Combo();
                        temp.CreateCombo(firstCardArray[i], secondCardArray[j]);
                        _convertedRange[_combosCounter] = temp;
                        _combosCounter++;
                    }
                }
            }
        }

        private void ConvertUnsuitedHand(Card[] firstCardArray, Card[] secondCardArray)
        {
            Combo temp = new Combo();

            for (int i = 0; i < firstCardArray.Length; i++)
            {
                for (int j = 0; j < secondCardArray.Length; j++)
                {
                    if (secondCardArray[j].CardSuit != firstCardArray[i].CardSuit)
                    {
                        temp = new Combo();
                        temp.CreateCombo(firstCardArray[i], secondCardArray[j]);
                        _convertedRange[_combosCounter] = temp;
                        _combosCounter++;
                    }
                }
            }
        }

        // Метод определяет тип руки и вызывает соответсвующие
        // этому типу методы для последующей конвертации
        private void DeffineHandType(string cards)
        {
            char firstCard = cards[0];
            char secondCard = cards[1];

            if (firstCard == secondCard)
            {
                PushHandInstances(firstCard, _firstCardArray);
                ConvertPocketPairs(_firstCardArray);
            }
            else
            {
                PushHandInstances(firstCard, _firstCardArray);
                PushHandInstances(secondCard, _secondCardArray);
                ConvertUnpaired(_firstCardArray, _secondCardArray);
            }
        }

        private void DeffineHandType(char firstCard, char secondCard)
        {
            if (firstCard == secondCard)
            {
                PushHandInstances(firstCard, _firstCardArray);
                ConvertPocketPairs(_firstCardArray);
            }
            else
            {
                PushHandInstances(firstCard, _firstCardArray);
                PushHandInstances(secondCard, _secondCardArray);
                ConvertUnpaired(_firstCardArray, _secondCardArray);
            }
        }

        private void FormatString(string range)
        {
            string[] convertResult = range.Split(',');
            for (var i = 0; i < convertResult.Length; i++)
            {
                range = convertResult[i];
                DeffineHandGroup(range);
            }
        }


        public void FormatFile(string fileLocation)
        {
            string path = fileLocation;
            string range = File.ReadAllText(path);

            var removePossition = range.IndexOf('\r');

            if (removePossition > 0)
            {
                range = range.Remove(removePossition);
            }

            _range = range;

            string[] convertResult = range.Split(',');
            for (var i = 0; i < convertResult.Length; i++)
            {
                range = convertResult[i];
                DeffineHandGroup(range);
            }
        }

        private void DeffineHandGroup(string range)
        {
            var size = range.Length;

            switch (size)
            {
                case 2: DeffineHandType(range);
                    break;
                case 3: DeffineIfSuited(range);
                    break;
                case 5: ExtractRange(range);
                    break;
                case 7: ExtractHugeFormRange(range);
                    break;
                default:
                    break;
            }
        }

        private void DeffineIfSuited(string range)
        {
            PushHandInstances(range[0], _firstCardArray);
            PushHandInstances(range[1], _secondCardArray);

            switch (range[2])
            {
                case 's': ConvertSuitedHand(_firstCardArray, _secondCardArray);
                    break;
                case 'o': ConvertUnsuitedHand(_firstCardArray, _secondCardArray);
                    break;
                default:
                    break;
            }
        }


        private void ExtractRange(string range)
        {
            if (range[0] == range[1])
            {
                ExtractPairedRange(range);
            }
            else
            {
                ExtractUnpairedRange(range);
            }
        }

        private int ExtractPairedRange(string range)
        {
            for (int i = 0; i < _faceArray.Length; i++)
            {
                if (range[3] == _faceArray[i])
                {
                    var j = i;
                    while (_faceArray[j] != range[0])
                    {
                        DeffineHandType(_faceArray[j], _faceArray[j]);
                        j++;
                    }

                    DeffineHandType(range[0], range[1]);
                    return 0;
                }
            }

            return 0;
        }

        private int ExtractUnpairedRange(string range)
        {
            for (int i = 0; i < _faceArray.Length; i++)
            {
                if (_faceArray[i] == range[4])
                {
                    var j = i;
                    while (_faceArray[j] != range[0])
                    {
                        DeffineHandType(range[0], _faceArray[j]);
                        j++;
                    }
                    return 0;
                }
            }

            return 0;
        }

        private int ExtractHugeFormRange(string range)
        {
            PushHandInstances(range[0], _firstCardArray);

            if (range[2] == 's')
            {
                for (int i = 0; i < _faceArray.Length; i++)
                {
                    if (_faceArray[i] == range[5])
                    {
                        var j = i;
                        while (_faceArray[j] != range[0])
                        {
                            PushHandInstances(_faceArray[j], _secondCardArray);
                            ConvertSuitedHand(_firstCardArray, _secondCardArray);

                            if (_faceArray[j] == range[1])
                            {
                                return 0;
                            }

                            j++;
                        }
                    }
                }
            }

            else if (range[2] == 'o')
            {
                for (int i = 0; i < _faceArray.Length; i++)
                {
                    if (_faceArray[i] == range[5])
                    {
                        var j = i;
                        while (_faceArray[j] != range[0])
                        {
                            PushHandInstances(_faceArray[j], _secondCardArray);
                            ConvertUnsuitedHand(_firstCardArray, _secondCardArray);
                            if (_faceArray[j] == range[1])
                            {
                                return 0;
                            }

                            j++;
                        }
                    }
                }
            }

            return 0;
        }

        #endregion

        public bool CheckHeroAction(Card[] holeCards)
        {
            Combo temp = new Combo();

            bool comboFromRange = false;

            for (int i = 0; i < _combosCounter; i++)
            {
                temp = new Combo();
                temp = _convertedRange[i];
                comboFromRange = temp.CompareCombos(holeCards);

                if (comboFromRange == true)
                {
                    return comboFromRange;
                }
            }

            return comboFromRange;
        }

    }
}
