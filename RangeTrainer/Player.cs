using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace RangeTrainer
{
    internal class Player
    {
        public bool IsHero { get; set; }
        public string Name { get; set; }
        public double Stack { get; set; }
        public int CurrentPossition { get; set; }
        public bool IsActive { get; set; }
        public Card FirstCard { get; set; }
        public Card SecondCard { get; set; }

        public Player()
        {
            
        }

        public void Fold()
        {
            IsActive = false;
        }

        public void Call()
        {
            IsActive = true;
        }

        public void Raise()
        {
            IsActive = true;
        }

    }
}
