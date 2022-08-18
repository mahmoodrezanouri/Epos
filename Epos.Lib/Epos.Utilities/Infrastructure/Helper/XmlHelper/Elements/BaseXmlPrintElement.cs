using Epos.Utilities.Interfaces;
using System.Xml.Linq;

namespace Epos.Utilities.Infrastructure.Helper.XmlHelper.Elements
{
    public class BaseXmlPrintElement : Enumeration
    {
        protected static XNamespace nameSpace;
        private static XNamespace _soap = "http://schemas.xmlsoap.org/soap/envelope/";
        public string TagName { get; set; }

        public static XmlPrintTextElement EnvelopeElement = new XmlPrintTextElement(0, nameof(EnvelopeElement).ToLowerInvariant(), "Envelope");
        public static XmlPrintTextElement BodyElement = new XmlPrintTextElement(1, nameof(BodyElement).ToLowerInvariant(), "Body");

        public BaseXmlPrintElement(int id, string typeName , string tagName)
            : base(id, typeName)
        {
            TagName = tagName;
        }
        public virtual XElement CreateElement(IPrintDocumentElement documentElement)
        {
            return default(XElement);
        }
        public virtual XElement GetRootPrintElement()
        {
            return default(XElement);
        }
        public virtual XElement GetEnvelopeElement()
        {
            return new XElement(_soap + EnvelopeElement.TagName);
        }
        public virtual XElement GetBodyElement()
        {
            return new XElement(_soap + BodyElement.TagName);
        }
        public virtual XNamespace GetNameSpace()
        {
            return nameSpace;
        }
        public XElement CreateXmlRequest(IFluentPrintDocumentBuilder document)
        {
            var envelope = GetEnvelopeElement();
            var body = GetBodyElement();
            var print = GetRootPrintElement();

            envelope.Add(body);
            body.Add(print);

            var printDocuments = document.Build();

            foreach (var doc in printDocuments)
            {
                var el = CreateElement(doc);
                print.Add(el);
            }

            return envelope;
        }

    }

}


