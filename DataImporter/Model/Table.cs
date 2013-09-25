using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Table
    {
        public string Name { get; set; }
        public List<Relationship> Relationships { get; set; }
        public string Primarykey { get; set; }
        public Records Records { get; set; }
        public bool IsIdentityInsertOn { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
