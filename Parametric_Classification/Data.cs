using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parametric_Classification
{
    class Data
    {
        public List<double> Attributes { get; set; }
        public string ClassLabel { get; set; }


        public Data()
        {
            Attributes = new List<double>();
            ClassLabel = "";
        }

    }
}
