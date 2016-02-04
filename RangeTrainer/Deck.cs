using System;
using System.IO;
using System.Drawing;

namespace RangeTrainer
{
    internal class Deck
    {
        protected readonly Card[] _deck = new Card[52]; // my main Deck
        protected readonly char[] _faceArray = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
        private readonly char[] _suitType = { 'c', 'd', 'h', 's' };
        private readonly byte _index;

        // array for shuffeling. will return index for deck.CardIndex
        private readonly byte[] _indexArray = new byte[52];

        public Deck()
        {
            for (var j = 0; j < _suitType.Length; j++)
            {
                for (byte i = 0; i < _faceArray.Length; i++, _index++)
                {
                    _deck[_index].CardFace = _faceArray[i];
                    _deck[_index].CardSuit = _suitType[j];
                    _deck[_index].CardIndex = _index;
                }
            }

            for (byte i = 0; i < _indexArray.Length; i++)
            {
                _indexArray[i] = i;
            }
        }

        #region Showing indexes array
        //public void ShowIndexes()
        //{

        //    for (int i = 0; i < deck.Length; i++)
        //    {
        //        if (deck[indexArray[i]].CardSuit == 'c')
        //        {
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            Console.WriteLine("{0}{1}", deck[indexArray[i]].CardFace, deck[indexArray[i]].CardSuit);
        //        }
        //        else if (deck[indexArray[i]].CardSuit == 'd')
        //        {
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            Console.WriteLine("{0}{1}", deck[indexArray[i]].CardFace, deck[indexArray[i]].CardSuit);
        //        }
        //        else if (deck[indexArray[i]].CardSuit == 'h')
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine("{0}{1}", deck[indexArray[i]].CardFace, deck[indexArray[i]].CardSuit);
        //        }
        //        else if (deck[indexArray[i]].CardSuit == 's')
        //        {
        //            Console.ForegroundColor = ConsoleColor.Gray;
        //            Console.WriteLine("{0}{1}", deck[indexArray[i]].CardFace, deck[indexArray[i]].CardSuit);
        //        }
        //    }
        //}
        #endregion

        public void Shuffle()
        {
            var random = new Random();
            byte swap, rand;

            for (byte i = 0; i < _indexArray.Length; i++)
            {
                rand = (byte)random.Next(0, 51);

                swap = _indexArray[i];
                _indexArray[i] = _indexArray[rand];
                _indexArray[rand] = swap;
            }
        }
        

        #region Overloads of function Deal()

        public void Deal()
        {
            var myHoleCards = new Card[2];
            var flop = new Card[3];
            var turn = new Card[1];
            var river = new Card[1];

            for (var i = 0; i < myHoleCards.Length; i++)
            {
                myHoleCards[i] = _deck[_indexArray[i]];
            }

            for (var i = 2; i < flop.Length + 2; i++)
            {
                flop[i - 2] = _deck[_indexArray[i]];
            }

            turn[0] = _deck[_indexArray[5]];
            river[0] = _deck[_indexArray[6]];
        }

        public void Deal(string filePath)
        {
            var container = "";
            var myHoleCards = new Card[2];
            var flop = new Card[3];
            var turn = new Card[1];
            var river = new Card[1];

            for (var i = 0; i < myHoleCards.Length; i++)
            {
                myHoleCards[i] = _deck[_indexArray[i]];
                container += Convert.ToString(myHoleCards[i].CardFace);
                container += Convert.ToString(myHoleCards[i].CardSuit);
            }

            container += "   ";

            for (var i = 2; i < flop.Length + 2; i++)
            {
                flop[i - 2] = _deck[_indexArray[i]];
                container += Convert.ToString(_deck[_indexArray[i]].CardFace);
                container += Convert.ToString(_deck[_indexArray[i]].CardSuit);
            }

            container += " ";

            turn[0] = _deck[_indexArray[5]];

            container += turn[0].CardFace;
            container += turn[0].CardSuit;
            container += " ";

            river[0] = _deck[_indexArray[6]];

            container += river[0].CardFace;
            container += river[0].CardSuit;

            container += "\r";

            File.AppendAllText(filePath, container);
        }

        #endregion
        public Card[] DealHeroCards()
        {
            var heroCards = new Card[2];
            var container = "";

            for (var i = 0; i < heroCards.Length; i++)
            {
                heroCards[i] = _deck[_indexArray[i]];
                container += heroCards[i].CardFace.ToString();
                container += heroCards[i].CardSuit.ToString();
            }

            //Show(heroCards);

            return heroCards;
        }

        private void PutCardInLabel(Card card, System.Windows.Forms.Label label)
        {
            label.Text = Convert.ToString(card.CardFace);

            switch (card.CardSuit)
            {
                case 'c': label.BackColor = Color.FromArgb(38,173,95);
                    break;
                case 'd': label.BackColor = Color.FromArgb(65,165,224);
                    break;
                case 'h': label.BackColor = Color.FromArgb(217,83,79);
                    break;
                case 's': label.BackColor = Color.FromArgb(125,123,120);
                    break;
                default:
                    break;
            }
        }

        public void ShowHeroCards(Card[] deck, System.Windows.Forms.Label firstCard, System.Windows.Forms.Label secondCard)
        {
            PutCardInLabel(deck[0], firstCard);
            PutCardInLabel(deck[1], secondCard);
        }
    }
}