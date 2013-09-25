using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ExtensionMethods
    {
        public static string EncodeForInsertQuery(this string source)
        {
            return source.Replace("'", "''");
        }
    }
}
