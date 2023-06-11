using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManager.Objects
{
    public enum DataType
    {
        String = 0,
        Number = 1,
        DateTime = 2,
    }

    public class Input
    {
        public string Text { get; set; }
        public DataType DataType { get; set; }
    }
}
