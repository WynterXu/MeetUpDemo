using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetUpDemo.GetApiData;
using MeetUpDemo.StorgeCsv;

namespace MeetUpDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MeetUpApiData meetUpApiData = new MeetUpApiData();
            string resData = meetUpApiData.GetApiData();
            CSVFileHelper.SaveToCsv(resData);
        }
    }
}
