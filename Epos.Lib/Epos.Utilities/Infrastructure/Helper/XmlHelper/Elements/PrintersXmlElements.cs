using System;
using System.Collections.Generic;
using System.Linq;
using Epos.Utilities.Infrastructure.Helper.XmlHelper.Elements;

namespace Epos.Utilities.Infrastructure.XmlHelper.Helper.Elements
{
    public class PrintersXmlElements : BaseXmlPrintElement
    {
        public static EpsonXmlPrintElements EpsonXmlElements = new EpsonXmlPrintElements(0, nameof(EpsonXmlElements).ToLowerInvariant(), "EPSON");
 
        public PrintersXmlElements(int id, string typeName, string tagName)
           : base(id , typeName , tagName)
        {
        }

        public static IEnumerable<BaseXmlPrintElement> List()
        {
            var list = new[] {

               EpsonXmlElements
           };

            return list;
        }
        public static BaseXmlPrintElement FromName(string name)
        {
            var element = List().SingleOrDefault(s => string.Equals(s.TagName, name, StringComparison.CurrentCultureIgnoreCase));

            if (element == null)
            {
                throw new ArgumentException($"Possible values for PrintersXmlElements : {string.Join(",", List().Select(s => s.Name))}");
            }

            return element;
        }

    }
}
