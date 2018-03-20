using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parametric_Classification
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Data> dat = new List<Data>();
            List<string> classes = new List<string>();

            dat = ExtractData(@"c:\users\mathman\documents\visual studio 2015\Projects\Parametric_Classification\Parametric_Classification\Datasets\data1.txt", out classes);

        }

        public static List<Data> ExtractData(string FileName, out List<string> ClassPossibilities)
        {
            StreamReader reader = new StreamReader(FileName);
            string line = "";

            List<Data> dataset = new List<Data>();
            ClassPossibilities = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(' ');
                words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Data d = new Data();

                for (int i = 0; i < words.Length - 1; i++)
                {
                    d.Attributes.Add(Double.Parse(words[i]));
                }
                d.ClassLabel = words[words.Length - 1];
                dataset.Add(d);

                if (!ClassPossibilities.Contains(d.ClassLabel))
                {
                    ClassPossibilities.Add(d.ClassLabel);
                }

            }

            return dataset;

        }



        public static List<List<Data>> SplitClasses(List<Data> rawList, List<string> classes)
        {
            List<List<Data>> listoflists = new List<List<Data>>();
            foreach (string s in classes)
            {
                List<Data> dlist = new List<Data>();

                foreach (Data d in rawList)
                {
                    if (d.ClassLabel.Equals(s))
                    {
                        dlist.Add(d);

                    }
                }
                listoflists.Add(dlist);
            }

            return listoflists;

        }

    }
}
