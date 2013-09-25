using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Relationship
    {
        public string Name { get; set; }
        public string PrimaryKeyTable { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public string ForiegnKeyTable { get; set; }
        public string ForiegnKeyColumn { get; set; }
    }

    public class Relationships : List<Relationship>
    {
        
    }
}
