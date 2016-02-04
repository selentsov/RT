    using System;

namespace RangeTrainer
{
    class Combo
    {
        private Card[] _combo = new Card[2];


        public Card[] ComboProperties
        {
            get { return _combo; }
            set { _combo = value; }
        }

        public Card[] CreateCombo(Card firstCard, Card secondCard)
        {
            _combo[0].CardFace = firstCard.CardFace;
            _combo[0].CardSuit = firstCard.CardSuit;
            _combo[1].CardFace = secondCard.CardFace;
            _combo[1].CardSuit = secondCard.CardSuit;

            return _combo;
        }

        public bool CompareCombos(Card[] hand)
        {
            if ((_combo[0].CardFace == hand[0].CardFace && _combo[0].CardSuit == hand[0].CardSuit
                && _combo[1].CardFace == hand[1].CardFace && _combo[1].CardSuit == hand[1].CardSuit)
                || (_combo[0].CardFace == hand[1].CardFace && _combo[0].CardSuit == hand[1].CardSuit
                && _combo[1].CardFace == hand[0].CardFace && _combo[1].CardSuit == hand[0].CardSuit))
            {
                return true;
            }

            return false;
        }

        public void ShowCombo()
        {
            for (int i = 0; i < _combo.Length; i++)
            {
                Console.Write(_combo[i].CardFace);
                Console.Write(_combo[i].CardSuit);
            }
        }

        public string ComboToString()
        {
            string comboString = "";
            comboString += _combo[0].CardFace.ToString();
            comboString += _combo[0].CardSuit.ToString();
            comboString += _combo[1].CardFace.ToString();
            comboString += _combo[1].CardSuit.ToString();

            return comboString;
        }
    }
}