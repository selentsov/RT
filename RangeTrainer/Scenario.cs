using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTrainer
{
    class Scenario
    {
        private string[] _savedFilesPool = new string[0];

        #region Properties
        public string this[int index]
        {
            get { return _savedFilesPool[index]; }
            set { _savedFilesPool[index] = value; }
        }
        
        #endregion

        public void ReadFromFile()
        {
            
        }
    }
}
