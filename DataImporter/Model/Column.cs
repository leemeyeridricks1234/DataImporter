using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Column
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public int IsIdentity { get; set; }
    }
}
