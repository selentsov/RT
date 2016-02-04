using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTrainer
{
    class HandDeffiner : Deck
    {
        //private readonly int[] _hand = new int[7];
        private const string CombosFile = "combos.txt";
       // private readonly char[] _faceArray = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
       // private readonly char[] _suitType = { 'c', 'd', 'h', 's' };

        public HandDeffiner(int[] hand)
        {
            //for (var i = 0; i < hand.Length; i++)
            //{
            //    _hand[i] = hand[i];
            //}

            //Array.Sort(_hand);
        }

        public HandDeffiner()
        {
            DeffRoyalFlush();
            DeffStraightFlush();
            DeffQuads();
        }

        private long ConvertArrayToNumber(int[] array)
        {
            long result = 0;
            var swap = "";

            for (int i = 0; i < array.Length; i++)
            {
                swap += Convert.ToString(array[i]);
            }

            result = Convert.ToInt64(swap);

            File.AppendAllText(CombosFile,swap);
            File.AppendAllText(CombosFile, "\r");
            return result;
        }

        // royal flush
        private void DeffRoyalFlush()
        {
            int[] royalFlush = new int[5];

            for (int i = 0; i < _deck.Length; i++)
            {
                if (_deck[i].CardFace == 'T')
                {
                    for (int k = 0,j = i; k < royalFlush.Length; k++, j++)
                    {
                        royalFlush[k] = _deck[j].CardIndex;
                    }
                    ConvertArrayToNumber(royalFlush);
                }
            }
        }

        // straight flush
        private void DeffStraightFlush()
        {
            int[] straightFlush = new int[5];

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < _deck.Length; j++)
                {
                    if (_deck[j].CardFace == _faceArray[i])
                    {
                        for (int k = 0, t=j; k < straightFlush.Length; k++,t++)
                        {
                            straightFlush[k] = _deck[t].CardIndex;
                        }
                        ConvertArrayToNumber(straightFlush);
                    }
                }
            }

            for (int i = 0; i < _deck.Length; i++)
            {
                if (_deck[i].CardFace == '2')
                {
                    for (int k = 0, j = i; k < straightFlush.Length-1; k++, j++)
                    {
                        straightFlush[k] = _deck[j].CardIndex;
                    }
                    straightFlush[4] = _deck[i + 12].CardIndex;
                    Array.Sort(straightFlush);

                    ConvertArrayToNumber(straightFlush);
                }
            }
        }

        //Quads
        private void DeffQuads()
        {
            int[] quads = new int[5];

            for (int i = 12, k = 0; i >= 0; i--)
            {
                for (int j = 0; j < _deck.Length; j++)
                {
                    if (_deck[j].CardFace ==_faceArray[i])
                    {
                        quads[k] = _deck[j].CardIndex;
                        k++;
                    }
                }

                k = 0;

                for (int j = 12; j >= 0; j--)
                {
                    if (quads[0]!= _faceArray[j])
                    {
                        for (int l = 0; l < _deck.Length; l++)
                        {
                            if (_deck[l].CardFace == _faceArray[j])
                            {
                                quads[4] = _deck[l].CardIndex;
                                Array.Sort(quads);
                                ConvertArrayToNumber(quads);
                            }
                        }
                    }
                }
            }
        }
    }
}