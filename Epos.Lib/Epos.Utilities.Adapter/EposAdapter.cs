using Epos.Utilities.Adapter.Interfaces;
using Epos.Utilities.Infrastructure;
using Epos.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Epos.Utilities.Adapter.Model;

namespace Epos.Utilities.Adapter
{
    public class EposAdapter 
    {
        private ICollection<IPrintAdapter> _printAdapters = new List<IPrintAdapter>();

        public void Add(IPrintAdapter printer)
        {
            _printAdapters.Add(printer);
        }

        public void PrintAll()
        {
            foreach(var printer in _printAdapters)
            {
                printer.SendPrintRequest();
            }
        }

    }
}
