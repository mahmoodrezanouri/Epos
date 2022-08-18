using Epos.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Epos.Utilities.Adapter.Extensions;
using Newtonsoft.Json;

namespace Epos.Utilities.Adapter.Infrastructure
{
    public class PrintDocumentBuilder : IFluentPrintDocumentBuilder
    {
        [JsonProperty]
        private ICollection<IPrintDocumentElement> _printDocumentElements;

        private PrintDocumentBuilder()
        {
            _printDocumentElements = new HashSet<IPrintDocumentElement>();
        }

        public static IFluentPrintDocumentBuilder Start()
        {
            return new PrintDocumentBuilder();
        }

        public IFluentPrintDocumentBuilder PrintImage(string imageUrl)
        {
            _printDocumentElements.Add(new BaseImageDocumentElement() { Value = imageUrl });
            return this;
        }

        public IFluentPrintDocumentBuilder PrintLine(string text)
        {
            _printDocumentElements.Add(new BaseTextLineDocumentElement()
            {
                Value = text
            });

            return this;
        }
        public IFluentPrintDocumentBuilder SetAttribute(Action<TextAttribute> attribute)
        {
            var newtextAttribute = ObjectHelper.SetAction(attribute);

            var textLineEl = GetLastTextLineElement();

            if (textLineEl != null)
            {
                var attrList = textLineEl.Attributes.ToList();
                attrList.Add(newtextAttribute);
                textLineEl.Attributes = attrList;
            }

            return this;
        }
        public IFluentPrintDocumentBuilder SetStyle(Action<TextStyle> style)
        {
            var newStyle = ObjectHelper.SetAction(style);
            var textLineEl = GetLastTextLineElement();

            if (textLineEl != null)
            {
                textLineEl.Style = newStyle;
            }

            return this;
        }

        public IFluentPrintDocumentBuilder PrintNewLine()
        {
            _printDocumentElements.Add(new BaseNewLineDocumentElement());
            return this;
        }

        public ICollection<IPrintDocumentElement> Build()
        {
            return _printDocumentElements;
        }

        private BaseTextLineDocumentElement GetLastTextLineElement()
        {
            if (_printDocumentElements == null 
                || (_printDocumentElements != null && !_printDocumentElements.Any())
                || (_printDocumentElements != null && !_printDocumentElements.Any(x=> x.GetType() == typeof(BaseTextLineDocumentElement)) )
                )
            {
                return null;
            }

            return _printDocumentElements.LastOrDefault(x => x.GetType() == typeof(BaseTextLineDocumentElement)) as BaseTextLineDocumentElement;
        }


    }
}
