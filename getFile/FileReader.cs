using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace getFile
{
    class FileReader
    {
        public int Lines { get; set; }
        public String Text { get; set; }
        public String ProcessedText { get; set; }
        public Dictionary<String, int> WordsArray { get; set; }

        public FileReader(string path)
        {
            Lines = 0;
            String line;
            try
            {
                StreamReader sr = new StreamReader(path);
                line = sr.ReadLine();

                while (line != null)
                {
                    if (line != "")
                    {
                        Lines++;
                        Text = Text + " " + line;
                    }
                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            Text = Regex.Replace(Text, @"\s+", " ");
            SetParams();
        }
        

        private void SetParams()
        {
            ProcessedText = Text.ToLower();
            ProcessedText = Regex.Replace(ProcessedText, " a ", " ");
            ProcessedText = Regex.Replace(ProcessedText, " an ", " ");
            ProcessedText = Regex.Replace(ProcessedText, @"\s+", " ").Trim();
            string[] forChange = new string[] { "?", ";", ",", ".", "!", "(", "\"","'", ":", ")", "-" };
            ProcessedText = forChange.Aggregate(ProcessedText, (c1, c2) => c1.Replace(c2, ""));

            String[] arr = ProcessedText.Split(' ');

            WordsArray = new Dictionary<string, int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!WordsArray.ContainsKey(arr[i]))
                {
                    WordsArray.Add(arr[i], 1);
                }
                else
                {
                    WordsArray[arr[i]]++;
                }
            }
        }

        public int Task1()
        {
            return Lines;
        }

        public int Task2()
        {
            int numberWords = 0;
            numberWords = ProcessedText.Count(x => x == ' ') + 1;
            return numberWords;
        }

        public int Task3()
        {
            return WordsArray.Count();
        }

        public double[] Task4()
        {
            var text = Text.ToLower();
            text = Regex.Replace(text, " a ", " ");
            text = Regex.Replace(text, " an ", " ");
            string[] forChange = new string[] { "?", "!"};
            ProcessedText = forChange.Aggregate(ProcessedText, (c1, c2) => c1.Replace(c2, "."));
            text = Regex.Replace(text, @"\s+", " ").Trim();
            var arr = text.Split(". ");
            double max = 0;
            double avg = arr[0].Trim().Count(x => x == ' ') + 1;
            int num;
            for (int i = 0; i < arr.Length; i++)
            {
                num = arr[i].Count(x => x == ' ') + 1;
                avg = (avg + num) / 2;
                if (num > max)
                {
                    max = num;
                }

            }
            return new double[] { max, avg };

        }

        public String[] Task5()
        {

            int maxWhithSyntaxWords = 0;
            int maxWhithOutSyntaxWords = 0;
            String popularWord1 = "";
            String popularWord2 = "";

            foreach (var item in WordsArray)
            {
                if (item.Value > maxWhithSyntaxWords)
                {
                    maxWhithSyntaxWords = item.Value;
                    popularWord1 = item.Key;

                }else if (item.Value == maxWhithSyntaxWords)
                {
                    popularWord1 = popularWord1 + ", " + item.Key;
                }
                if (!IsSyntaxWord(item.Key))
                {
                    if (item.Value > maxWhithOutSyntaxWords)
                    {
                        maxWhithOutSyntaxWords = item.Value;
                        popularWord2 = item.Key;

                    }else if (item.Value == maxWhithOutSyntaxWords)
                    {
                        popularWord2 = popularWord2 + ", " + item.Key;
                    }
                }
            }
            return new String[] { popularWord1, popularWord2 };
        }

        public int Task6()
        {
            int startIndex = 0;
            int end = ProcessedText.IndexOf('k');
            end = ProcessedText.Substring(startIndex, end).LastIndexOf(" ");
            String sentence;
            int max = 0, num, l = 0;
            while (startIndex != -1)
            {
                
                
                sentence = ProcessedText.Substring(startIndex, end);
                num = sentence.Count(x => x == ' ') + 1;
                if (num > max)
                {
                    max = num;
                }
                l = end + 1 + startIndex;
                startIndex = ProcessedText.Substring(l).IndexOf(" ");
                if(startIndex != -1) {
                    startIndex = startIndex + l +1;
                    end = ProcessedText.Substring(startIndex).IndexOf('k') + 1;
                    if(end > 0)
                    {
                        end = ProcessedText.Substring(startIndex, end).LastIndexOf(" ");
                        if(end == -1)
                        {
                            end = 0;
                        }
                    }
                    else
                    {
                        end = ProcessedText.Length - startIndex - 1;
                    }

                }
               
                
                

            }
            return max;

        }
        private static Dictionary<string, long> numberTable = new Dictionary<string, long>
        {{"quintillion",1000000000000000000},{"quadrillion",1000000000000000}
            ,{"trillion",1000000000000},{"billion",1000000000},{"million",1000000},{"thousand",1000},
            {"hundred",100},{"ninety",90},{"eighty",80},{"seventy",70}
            ,{"sixty",60},{"fifty",50},{"forty",40},{"thirty",30},
        {"twenty",20},{"nineteen",19},{"eighteen",18},{"seventeen",17}
        ,{"sixteen",16},{"fifteen",15},{"fourteen",14},
         {"thirteen",13},{"twelve",12},{"eleven",11},{"ten",10}
            ,{"nine",9}, {"eight",8},{"seven",7},{"six",6},{"five",5},{"four",4},
            {"three",3},{"two",2},{"one",1},{"zero",0}

        };
        public static long ToLong(string numberString)
        {
            var numbers = Regex.Matches(numberString, @"\w+").Cast<Match>()
                 .Select(m => m.Value.ToLowerInvariant())
                 .Where(v => numberTable.ContainsKey(v))
                 .Select(v => numberTable[v]);
            long acc = 0, total = 0L;
            foreach (var n in numbers)
            {
                if (n >= 1000)
                {
                    total += (acc * n);
                    acc = 0;
                }
                else if (n >= 100)
                {
                    acc *= n;
                }
                else acc += n;
            }
            return (total + acc) * (numberString.StartsWith("minus",
                  StringComparison.InvariantCultureIgnoreCase) ? -1 : 1);
        }
        //public long Task7()
        //{
        //    string key = "";
        //    foreach (var item in numberTable)
        //    {
        //        if (ProcessedText.Contains(item.Key))
        //        {
        //            key = item.Key;
        //            break;
        //        }
        //    }
        //    long max = 0, num;
        //    if (key != "" && numberTable[key] >= 100)
        //    {
        //        int startIndex = 0;
        //        int end = ProcessedText.IndexOf(key);
        //        String before;
        //        int z;

        //        while (end != -1)
        //        {

        //            before = text.Substring(startIndex, end);
        //            z = before.LastIndexOf(' ');
        //            before = text.Substring(z, end);
        //            num = ToLong(before + key);
        //            if (num > max1)
        //            {
        //                max1 = num;
        //            }
        //            startIndex = end;
        //            end = text.Substring(end).IndexOf(key);


        //        }
        //    }
        //    foreach (var item in dic)
        //    {
        //        if (item.Key.All(char.IsDigit))
        //        {
        //            num = Convert.ToInt64(item.Key);
        //            if (num > max1)
        //            {
        //                max1 = num;
        //            }
        //        }
        //    }


        //}

        public List<string> Task8( )
        {
            string[] colors = Enum.GetNames(typeof(KnownColor));
            colors = colors.Select(c => c.ToLowerInvariant()).ToArray();
            List<string> colorsList = new List<string>(colors);
            colorsList.Sort();
            List<string> existColorsList = new List<string>();
            foreach (var item in WordsArray)
            {
                if (colorsList.BinarySearch(item.Key) > -1)
                {
                    existColorsList.Add(item.Key + ": " + item.Value);
                }
            }
            return existColorsList;
        }


        private bool IsSyntaxWord(string word)
        {
            var arr = new String[] { "am", "are", "don't", "that", "the" ,"is","isn't", "of", "aren't","there","to", "amn't", "and", "didn't", "was", "were", "wasn't", "weren't" };
            return arr.Contains(word);
        }

        

        
    }
}
