using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RangeTrainer
{
    class ActionRange : Deck
    {
        private RangeCreator _raise;
        private RangeCreator _call;
        private RangeCreator _plussed;
        private string _rangesFile = "tmp.txt";

        #region Constructors
        public ActionRange()
        {
            var raiseRange = ReadRangeFromFile(_rangesFile, "<Raise>", "</Raise>");
            var callRange = ReadRangeFromFile(_rangesFile, "<Call>", "</Call>");

            _raise = new RangeCreator(raiseRange);
            _call = new RangeCreator(callRange);

        }
        
        #endregion

        #region Properties
        public RangeCreator Raise
        {
            get { return _raise; }
        }

        public RangeCreator Call
        {
            get { return _call; }
        }

        public RangeCreator Fold
        {
            get { return _plussed; }
        }
        
        #endregion

        private string ReadRangeFromFile(string path, string startTag, string finishTag)
        {
            var source = File.ReadAllText(path);
            var startIndex = source.IndexOf(startTag);
            var finishIndex = source.IndexOf(finishTag);

            startIndex += startTag.Length;

            if (finishIndex > 0)
            {
                source = source.Substring(startIndex, finishIndex - startIndex);
               return source;
            }
            else
            {
                return "";
            }

        }

    }
}