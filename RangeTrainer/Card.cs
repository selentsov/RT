using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTrainer
{

    internal struct Card
    {
        private char _cardFace;
        private char _cardSuit;
        private byte _cardIndex;

        public Card(char cardFace, char cardSuit, byte cardIndex)
        {
            _cardFace = cardFace;
            _cardSuit = cardSuit;
            _cardIndex = cardIndex;
        }

        #region Properties

        public char CardFace
        {
            get { return _cardFace; }
            set { _cardFace = value; }
        }

        public char CardSuit
        {
            get { return _cardSuit; }
            set { _cardSuit = value; }
        }

        public byte CardIndex
        {
            get { return _cardIndex; }
            set { _cardIndex = value; }
        }

        #endregion
    }
}
