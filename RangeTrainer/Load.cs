using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RangeTrainer
{
    class Load
    {
        private string _saveFile;
        private string _raiseRangeSavingPath = "tmp.txt";
        private string _callRangeSavingPath = "tmp.txt";
        //private string _possition;

        public Load(string saveFile)
        {
            _saveFile = saveFile;
            LoadInfo();
        }

        private string Read(string path, string startTag, string finishTag)
        {
            var source = File.ReadAllText(path);
            var startIndex = source.IndexOf(startTag);
            var finishIndex = source.IndexOf(finishTag);

            startIndex += startTag.Length;
            var result = source.Substring(startIndex, finishIndex - startIndex);

            //File.WriteAllText("debug.txt",result);
            return result;
        }


        public void LoadInfo()
        {
            var raiseRange = Read(_saveFile,"<Raise>","</Raise>");
            var callRange = Read(_saveFile, "<Call>", "</Call>");
        }
    }
}
