using System;
using System.Collections.Generic;
using System.Text;

namespace Epos.Utilities.Interfaces
{
    public interface IFluentPrintDocumentBuilder
    {
        IFluentPrintDocumentBuilder PrintLine(string text);
        IFluentPrintDocumentBuilder SetStyle(Action<TextStyle> style);
        IFluentPrintDocumentBuilder SetAttribute(Action<TextAttribute> attribute);
        IFluentPrintDocumentBuilder PrintNewLine();
        IFluentPrintDocumentBuilder PrintImage(string text);
        ICollection<IPrintDocumentElement> Build();
    }
    public class TextStyle : IPrintDocumentElementAttribute
    {
        public string Width { get; set; }
        public string Height { get; set; }
    }
    public class TextAttribute : IPrintDocumentElementAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class BaseTextLineDocumentElement : IPrintDocumentElement
    {
        public string Value { get; set; }
        public IEnumerable<TextAttribute> Attributes { get; set; }
        public TextStyle Style { get; set; }
    }
    public class BaseImageDocumentElement : IPrintDocumentElement
    {
        public string Value { get; set; }
    }
    public class BaseNewLineDocumentElement : IPrintDocumentElement
    {
    }
}
