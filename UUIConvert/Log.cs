using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace UUIConvert
{
    public static class Log
    {
        public static void Info( string info )
        {
            System.Diagnostics.Trace.WriteLine(info);
        }

        public static void Exception(string info, Exception ex )
        {
            System.Diagnostics.Trace.WriteLine(info + ex.ToString());
        }
    }
}
