using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Condition
    {
        public ConditionalOperator Operator { get; set; }
        public string Column { get; set; }
        public string Value { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
    }
}
