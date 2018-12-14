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
//        private static List<string> bloodTypes = new List<string>(){ "A", "B", "O", "AB" };
//        private static List<string[]> bloodType_A = new List<string[]>();
//        private static List<string[]> bloodType_B = new List<string[]>();
//        private static List<string[]> bloodType_O = new List<string[]>();
//        private static List<string[]> bloodType_AB = new List<string[]>();
//        private static List<string[]>[] bloodTypeArray = {bloodType_A, bloodType_B, bloodType_O, bloodType_AB};
//
//        private static List<SortedList<string, List<string>>> bloodRhList = new List<SortedList<string, List<string>>>(8);
//        private static List<IList<List<string>>> _bloodRhData = new List<IList<List<string>>>();

        public const string Text_path = @"C:\Users\tiany\Desktop\blood bank\blood bank the app\blood bank app\blood bank app\bin\Debug\database\DonerData.txt";
        public const string CSV_path = @"C:\Users\tiany\Desktop\blood bank\blood bank the app\blood bank app\blood bank app\bin\Debug\database\DonerData";
        public static string[] CSV_headers = { "Last Name", "First Name", "Blood Group", "Rh Factor", "Address", "Phone" };
        

        public static void storeData(string[] DonerData)
        {
//            var index = bloodTypes.IndexOf(DonerData[2]);
//            bloodTypeArray[index].Add(DonerData);
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
            //extract readable data from dictionaries
//            _donerData = donerDataSortedList.Values;
//            foreach (var d in _donerData)
//                File.AppendAllText(Text_path, string.Join(",", d) + Environment.NewLine);
//
////            for (int i = 0; i < bloodRhTypes.Length; i++)
////            {
////                File.AppendAllText(Text_path, bloodRhTypes[i] + ":" + Environment.NewLine);
////                _bloodRhData.Add(bloodRhList[i].Values);
////                foreach (var j in _bloodRhData[i])
////                    File.AppendAllText(Text_path, string.Join(",", j) + Environment.NewLine);
////            }
//
//            for(int i = 0; i < bloodTypeArray.Length; i++)
//            {
//                File.AppendAllText(Text_path, bloodTypes[i] + ":" + bloodTypeArray[i].Count + Environment.NewLine);
//            }
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

            /*
            for (int i = 0; i < bloodRhTypes.Length; i++)
            {
                using (var CSV_DataByBloodRhFile = new StreamWriter(CSV_path + "_" + bloodRhTypes[i] + ".csv"))
                {
                    var CSV_DataByBloodRh = new CsvWriter(CSV_DataByBloodRhFile);
                    foreach (var f in _bloodRhData[i])
                    {
                        foreach (var g in f)
                        {
                            CSV_DataByBloodRh.WriteField(g);
                        }
                        CSV_DataByBloodRh.NextRecord();
                    }
                }
            }

            using (var CSV_BloodTypeCountFile = new StreamWriter(CSV_path + "_Blood Type Count.csv"))
            {
                var CSV_BloodTypeCount = new CsvWriter(CSV_BloodTypeCountFile);
                for (int i = 0; i < bloodTypes.Length; i++)
                {
                    CSV_BloodTypeCount.WriteField(bloodTypes[i]);
                    CSV_BloodTypeCount.WriteField(BloodTypeCount[i]);
                    CSV_BloodTypeCount.NextRecord();
                }
            }*/
        }
    }
}