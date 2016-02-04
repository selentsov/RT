using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace RangeTrainer
{
    class Result
    {
        private DateTime _date;
        private DateTime _finish;
        private TimeSpan _timePlayed;
        private string _path = "result.txt";
        private string _result = "";

        public void Start()
        {
            _date = DateTime.Now;
        }

        public void Finish(double result)
        {
            _result += _date.ToString();
            _result += "  ";
            _finish = DateTime.Now;
            _timePlayed = (_finish - _date);

            _result += _timePlayed.Minutes.ToString();
            _result += "   ";
            _result += result;
            _result += "\r";
            File.AppendAllText(_path,_result);
        }
    }
}
