using System;
using System.Collections.Generic;
using System.Text;

namespace Epos.Utilities.Test.Model
{
    public class PrintTestModel
    {
       public string TestName { get; set; }
       public string ModelName { get; set; }
       public string BrandName { get; set; }
       public string IpAddress { get; set; }
       public string PrinterName { get; set; }
       public int Port { get; set; }
       public int TimeOut { get; set; }
       public string TextToPrint { get; set; }
       public string Expected { get; set; }
    }
}
