using Epos.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Epos.Utilities.Infrastructure.Helper.XmlHelper.Elements;

namespace Epos.Utilities.Infrastructure.XmlHelper.Helper
{
    public class EpsonXmlPrintElements : BaseXmlPrintElement
    {
        private XNamespace _eposNameSpace => "http://www.epson-pos.com/schemas/2011/03/epos-print";

        public static XmlPrintTextElement RootPrintElement = new XmlPrintTextElement(0, nameof(RootPrintElement).ToLowerInvariant(), "epos-print");
        public static XmlPrintTextElement Text = new XmlPrintTextElement(1, typeof(BaseTextLineDocumentElement).Name.ToLowerInvariant(), "text");
        public static XmlPrintImageElement Image = new XmlPrintImageElement(2, typeof(BaseImageDocumentElement).Name.ToLowerInvariant(), "image");
        public static XmlPrintNewLineElement NewLine = new XmlPrintNewLineElement(3, typeof(BaseNewLineDocumentElement).Name.ToLowerInvariant(), string.Empty);

        public EpsonXmlPrintElements(int id, string typeName, string tagName)
           : base(id , typeName , tagName)
        {
            nameSpace = _eposNameSpace;
        }

        public static IEnumerable<BaseXmlPrintElement> List()
        {
            var list = new BaseXmlPrintElement[] {

               RootPrintElement,
               Text,
               Image,
               NewLine
           };

            return list;

        }
        public static BaseXmlPrintElement FromTypeName(string typeName)
        {
            var el = List().SingleOrDefault(s => string.Equals(s.Name, typeName, StringComparison.CurrentCultureIgnoreCase));

            if (el == null)
            {
                throw new ArgumentException($"Possible values for EpsonXmlPrintElement : {string.Join(",", List().Select(s => s.Name))}");
            }

            return el;
        }
        public override XElement CreateElement(IPrintDocumentElement documentElement)
        {
            var el = FromTypeName(documentElement.GetType().Name.ToLowerInvariant());
            return el.CreateElement(documentElement);
        }
        public override XElement GetRootPrintElement()
        {
            var rootPrintElement = new XElement(nameSpace + RootPrintElement.TagName);
            return rootPrintElement;
        }
        public override XNamespace GetNameSpace()
        {
            return nameSpace;
        }

    }
}
