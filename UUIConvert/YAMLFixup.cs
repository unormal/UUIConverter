using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Text.RegularExpressions;

namespace UUIConvert
{
    public static class YAMLFixup
    {
        public static string FixBlankKeys( string info )
        {
            StringBuilder result = new StringBuilder();

            int nChunk = 0;
            foreach( var chunk in info.Split('\n'))
            {
                if( Regex.Match( chunk, @"^\s*:" ).Success )
                {
                    result.Append($"__null_{(nChunk++)}");
                }
                result.Append(chunk);
                result.Append("\n");
            }

            return Regex.Replace(result.ToString(), "([0-9]+ &[0-9]+) stripped\n", "$1\n");
        }
    }
}
