using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace blood_bankblood_bank_app
{
    class database
    {
        private static SortedList<string, string[]> donerDataSortedList;
        private static IList<string[]> _donerData;
        public const string Text_path = @"C:\Users\tiany\Desktop\blood bank\blood bank the app\blood bank app\blood bank app\bin\Debug\database\DonerData.txt";
        public const string CSV_path = @"C:\Users\tiany\Desktop\blood bank\blood bank the app\blood bank app\blood bank app\bin\Debug\database\DonerData";
        public static string[] CSV_headers = { "Last Name", "First Name", "Blood Group", "Rh Factor", "Address", "Phone" };
        

        public static void storeData(string[] DonerData)
        {
            writeToTextFile(DonerData);
        }

        public static void readData()
        {
            donerDataSortedList = new SortedList<string, string[]>();
            string[] rawData = File.ReadAllLines(Text_path);
            foreach (var data in rawData)
            {
                string[] DonerData = data.Split(',');
                string name = DonerData[0] + DonerData[1];
                donerDataSortedList.Add(name, DonerData);
            }

            _donerData = donerDataSortedList.Values;
        }

        private static void writeToTextFile(String[] DonerData)
        {
            File.AppendAllText(Text_path, string.Join(",", DonerData) + Environment.NewLine);
        }

        public static string[] searchDataBase(string firstName, string lastName)
        {
            readData();
            var name = lastName + firstName;
            var index = donerDataSortedList.IndexOfKey(name);
            if (index >= 0)
            {
                return _donerData[index];
            }
            else
            {
                return null;
            }
        }

        public static List<string[]> findMatch(string bloodType, string RhFactor)
        {
            readData();
            var matches = new List<string[]>();
            foreach(var doner in _donerData)
            {
                if (doner[2] == bloodType && doner[3] == RhFactor)
                {
                    matches.Add(doner);
                }
            }
            return matches;
        }

        public static int[] bloodStorage()
        {
            readData();
            int[] bloodTypes = {0,0,0,0};
            foreach (var data in _donerData)
            {
                if (data[2] == "A")
                    bloodTypes[0] += 1;
                else if (data[2] == "B")
                    bloodTypes[1] += 1;
                else if (data[2] == "O")
                    bloodTypes[2] += 1;
                else if (data[2] == "AB")
                    bloodTypes[3] += 1; 
            }

            return bloodTypes;
        }

        public static void writeToCSVFile()
        {
            readData();
            using (var CSV_Data = new StreamWriter(CSV_path + ".csv"))
            {
                var CSV_DonerData = new CsvWriter(CSV_Data);
                foreach (var a in CSV_headers)
                    CSV_DonerData.WriteField(a);
                CSV_DonerData.NextRecord();
                foreach (var d in _donerData)
                {
                    foreach (var e in d)
                    {
                        CSV_DonerData.WriteField(e);
                    }
                    CSV_DonerData.NextRecord();
                }
            }
        }
    }
}
