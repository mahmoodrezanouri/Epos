using Epos.Utilities.Interfaces;
using System.Linq;
using System.Xml.Linq;
using Epos.Utilities.Infrastructure.Extensions;

namespace Epos.Utilities.Infrastructure.Helper.XmlHelper.Elements
{
    public class XmlPrintTextElement : BaseXmlPrintElement
    {
        public XmlPrintTextElement(int id, string typeName, string tagName)
            : base(id, typeName, tagName)
        {

        }
        public override XElement CreateElement(IPrintDocumentElement documentElement)
        {
            var textLineDocumentElement = (BaseTextLineDocumentElement)documentElement;

            var textEl = new XElement(nameSpace + TagName);
            textEl.Add(textLineDocumentElement.Value);

            if(textLineDocumentElement.Attributes != null && textLineDocumentElement.Attributes.Any())
            {
                foreach (var attr in textLineDocumentElement.Attributes)
                {
                    textEl.Add(new XAttribute(attr.Name, attr.Value));
                }
            }
            if (textLineDocumentElement.Style != null)
            {
                var propsInfo = textLineDocumentElement.Style.GetProperties();
                foreach(var prop in propsInfo)
                {
                    textEl.Add(new XAttribute(prop.Key, prop.Value));
                }
            }

            return textEl;
        }

    }

}


